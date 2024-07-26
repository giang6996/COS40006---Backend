using Server.Common.Models;

namespace Server.DataAccess.Interfaces
{
    public interface ITokenRepository
    {
        Task AddRefreshTokenAsync(RefreshToken refreshToken);
        Task AddAccessTokenAsync(AccessToken accessToken);
        Task<RefreshToken?> GetRefreshTokenAsync(string refreshTokenValue);
        Task<AccessToken?> GetAccessTokenAsync(string refreshTokenValue);
        Task RevokeAccessToken(AccessToken token);
        Task<Account> FetchAccountFromDb(string accessToken);
    }
}