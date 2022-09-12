using ISPH.Domain.Models.Base;
using ISPH.Shared.Dtos.Authorization;
using ISPH.Shared.Dtos.Users.Base;

namespace ISPH.Shared.Interfaces.Base;
public interface IUserService<TUser, TRegister, in TUpdate, TId> : ICrudService<TUser, TRegister, TUpdate, TId> 
    where TUser : BaseUser<TId> where TRegister : IDto<TId> where TUpdate : UserUpdateDto<TId>, IDto<TId> where TId : struct
{
    Task<TokensDto> RegisterAsync(TRegister user, CancellationToken token =  default);
    Task<TokensDto> LoginAsync(string email, string password, CancellationToken token =  default);
    Task<TokensDto> RefreshTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken = default);
    Task RevokeTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken = default);
    Task MessageUpdateEmailAsync(TId userId, CancellationToken token = default);
}