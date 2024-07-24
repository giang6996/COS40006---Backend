using Server.Common.Models;

namespace Server.DataAccess.Interfaces
{
    public interface ITokenService
    {
        Task AddRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken?> GetRefreshTokenAsync(string refreshTokenValue);
        Task AddAccessTokenAsync(AccessToken accessToken);
    }
}