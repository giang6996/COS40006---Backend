using Microsoft.EntityFrameworkCore;
using Server.Common.Models;
using Server.DataAccess.Interfaces;

namespace Server.DataAccess.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly AppDbContext _db;

        public TokenRepository(AppDbContext db)
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

        public async Task<Account> FetchAccountFromDb(string accessToken)
        {
            return await _db.AccessTokens
                .Where(at => at.Value == accessToken)
                .Join(
                    _db.RefreshTokens,
                    at => at.RtId,
                    rt => rt.Id,
                    (at, rt) => rt
                )
                .Join(
                    _db.Accounts.Include(a => a.AccountRoles)
                                .ThenInclude(ar => ar.Role)
                                .Include(a => a.Documents)
                                .ThenInclude(d => d.DocumentDetails)
                                .Include(a => a.Documents)
                                .ThenInclude(d => d.Apartment)
                                .Include(a => a.Documents)
                                .ThenInclude(d => d.Building),
                    rt => rt.AccountId,
                    a => a.Id,
                    (rt, a) => a
                )
                .FirstOrDefaultAsync() ?? throw new Exception("Account not found from this access token");
        }

        //public async Task<Account> FetchAccountFromDb(string accessToken)
        //{
        //    return await (from at in _db.AccessTokens
        //                  join rt in _db.RefreshTokens on at.RtId equals rt.Id
        //                  join a in _db.Accounts on rt.AccountId equals a.Id
        //                  where at.Value == accessToken
        //                  select a).FirstOrDefaultAsync() ?? throw new Exception("Account not found from this access token");
        //}
    }
}