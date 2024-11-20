using Microsoft.EntityFrameworkCore;
using Server.Common.Models;
using Server.DataAccess.Interfaces;

namespace Server.DataAccess.Repositories
{
    public class ResidentRepository : IResidentRepository
    {
        private readonly AppDbContext _db;

        public ResidentRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Account>> FetchAllNewAccountsWithDocsSubmitted()
        {
            return await (
                from a in _db.Accounts
                join d in _db.Documents on a.Id equals d.AccountId
                group a by a into g
                where g.Count() > 0
                select g.Key
            ).ToListAsync();
        }

        public async Task DeleteDocumentsByAccountId(long accountId)
        {
            try
            {
                // Step 1: Fetch all RefreshTokens related to the account
                var refreshTokens = await _db.RefreshTokens
                    .Where(rt => rt.AccountId == accountId)
                    .ToListAsync();

                // Step 2: Identify AccessTokens linked to these RefreshTokens by RefreshTokenId
                var refreshTokenIds = refreshTokens.Select(rt => rt.Id).ToList();

                var accessTokens = await _db.AccessTokens
                    .Where(at => refreshTokenIds.Contains(at.RtId))
                    .ToListAsync();

                // Step 3: Delete AccessTokens and RefreshTokens
                _db.AccessTokens.RemoveRange(accessTokens);
                _db.RefreshTokens.RemoveRange(refreshTokens);

                // Step 4: Fetch and delete DocumentDetails and Documents associated with the account
                var documents = await _db.Documents
                    .Where(d => d.AccountId == accountId)
                    .ToListAsync();

                var documentIds = documents.Select(d => d.Id).ToList();

                var documentDetails = await _db.DocumentDetails
                    .Where(dd => documentIds.Contains(dd.DocumentId))
                    .ToListAsync();

                _db.DocumentDetails.RemoveRange(documentDetails);
                _db.Documents.RemoveRange(documents);

                // Step 5: Fetch and delete the Account record itself
                var account = await _db.Accounts.FindAsync(accountId);
                if (account != null)
                {
                    _db.Accounts.Remove(account);
                }

                // Save all changes to apply deletions
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete account and associated data", ex);
            }
        }

        public async Task DeleteAccountAsync(Account account)
        {
            _db.Accounts.Remove(account);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateAsync(Account account)
        {
            _db.Accounts.Update(account);
            await _db.SaveChangesAsync();
        }
    }
}