using Server.Common;

namespace Server.DataAccess.Repositories
{
    public interface ITokenService
    {
        Task AddRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken?> GetRefreshTokenAsync(string refreshTokenValue);
        Task AddAccessTokenAsync(AccessToken accessToken);
    }
}