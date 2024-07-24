using Microsoft.EntityFrameworkCore;
using Server.Common.Models;
using Server.DataAccess.Interfaces;

namespace Server.DataAccess.Repositories
{
    public class TokenService : ITokenService
    {
        private readonly AppDbContext _db;

        public TokenService(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            _db.RefreshTokens.Add(refreshToken);
            await _db.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string refreshTokenValue)
        {
            return await _db.RefreshTokens.Where(r => r.Value == refreshTokenValue).FirstOrDefaultAsync();
        }

        public async Task<AccessToken?> GetAccessTokenAsync(string accessTokenValue)
        {
            return await _db.AccessTokens.Where(a => a.Value == accessTokenValue).FirstOrDefaultAsync();
        }

        public async Task AddAccessTokenAsync(AccessToken accessToken)
        {
            _db.AccessTokens.Add(accessToken);
            await _db.SaveChangesAsync();
        }

        public async Task RevokeAccessToken(AccessToken token)
        {
            token.Revoked = true;
            await _db.SaveChangesAsync();
        }
    }
}