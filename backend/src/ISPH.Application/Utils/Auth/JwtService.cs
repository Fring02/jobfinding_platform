using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ISPH.Domain.Configuration;
using ISPH.Domain.Models.Base;
using ISPH.Domain.Models.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ISPH.Application.Utils.Auth;

public class JwtService
{
    private readonly AuthenticationOptions _options;
    public JwtService(IOptions<AuthenticationOptions> options) => _options = options.Value;

    public string CreateToken(ClaimsIdentity identity)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret));
        var token = new JwtSecurityToken(
            claims: identity.Claims,
            expires: DateTime.Now.AddHours(6).AddDays(_options.AccessTokenLifetime),
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        );
        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(token);
    }

    public string CreateRefreshToken()
    {
        var rand = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(rand);
        return Convert.ToBase64String(rand);
    }

    public JwtSecurityToken ParseToken(string token) => new(token);

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false, ValidateIssuer = false,
            ValidateIssuerSigningKey = true, IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret)),
            ValidateLifetime = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || jwtSecurityToken.Header.Alg != SecurityAlgorithms.HmacSha256Signature)
            throw new SecurityTokenException("Invalid token");
        return principal;
    }
    public ClaimsIdentity CreateIdentity<TUser, TId>(TUser user) where TUser : BaseUser<TId>
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role),
        };
        if (typeof(TUser) == typeof(Employer))
        {
            claims.Add(new(ClaimTypes.UserData, typeof(TUser).GetProperty("CompanyId")!.GetValue(user)!.ToString()!));
        }
        return new ClaimsIdentity(claims);
    }
}