using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace ISPH.Domain.Models.Base;

public abstract class BaseUser<TId> : BaseEntity<TId>
{
    public string Email { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; } = null!;
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;
    [MaxLength(100)]
    public string Patronymic { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public abstract string Role { get; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    public byte[] HashedPassword { get; set; } = null!;
    public byte[] SaltPassword { get; set; } = null!;
    public void CreatePassword(string password)
    {
        using var hmac = new HMACSHA512();
        SaltPassword = hmac.Key;
        HashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
    public bool VerifyPassword(string password)
    {
        using var hmac = new HMACSHA512(SaltPassword);
        return hmac.ComputeHash(Encoding.UTF8.GetBytes(password)).SequenceEqual(HashedPassword);
    }
}