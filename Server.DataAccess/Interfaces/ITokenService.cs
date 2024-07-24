using Server.Common.Models;

namespace Server.DataAccess.Interfaces
{
    public interface ITokenService
    {
        Task AddRefreshTokenAsync(RefreshToken refreshToken);
        Task AddAccessTokenAsync(AccessToken accessToken);
        Task<RefreshToken?> GetRefreshTokenAsync(string refreshTokenValue);
        Task<AccessToken?> GetAccessTokenAsync(string refreshTokenValue);
        Task RevokeAccessToken(AccessToken token);
    }
}