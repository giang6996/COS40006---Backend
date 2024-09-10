using Microsoft.EntityFrameworkCore;
using Server.Common.DTOs;
using Server.Common.Models;
using Server.DataAccess.Interfaces;

namespace Server.DataAccess.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly AppDbContext _db;

        public FormRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddNewFormAsync(FormResidentRequest formResidentRequest)
        {
            _db.FormResidentRequests.Add(formResidentRequest);
            await _db.SaveChangesAsync();
        }

        public async Task AddNewFormDetailAsync(FormResidentRequestDetail formResidentRequestDetail)
        {
            _db.FormResidentRequestDetails.Add(formResidentRequestDetail);
            await _db.SaveChangesAsync();
        }

        public async Task<List<FormResponse>> GetAllFormRequest(string? status, string? label, string? type)
        {
            var query = from f in _db.FormResidentRequests
                        join fd in _db.FormResidentRequestDetails on f.Id equals fd.FormResidentRequestId
                        join a in _db.Accounts on f.AccountId equals a.Id
                        select new
                        {
                            a,
                            f,
                            fd
                        };

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(r => r.fd.Status == status);
            }

            if (!string.IsNullOrEmpty(label))
            {
                query = query.Where(r => r.fd.Label == label);
            }

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(r => r.fd.Type == type);
            }

            var result = await query.ToListAsync();

            List<FormResponse> formResponses = result.Select(r => new FormResponse
            {
                ResidentName = r.a.Email,
                Title = r.fd.Title,
                Description = r.fd.Description,
                Status = r.fd.Status,
                Label = r.fd.Label,
                Type = r.fd.Type,
                CreateAt = r.f.Timestamp
            }).ToList();

            return formResponses;
        }

        public async Task<List<FormResponse>> GetAllFormRequestByAccount(Account account, string? status, string? label, string? type)
        {
            var query = from f in _db.FormResidentRequests
                        join fd in _db.FormResidentRequestDetails on f.Id equals fd.FormResidentRequestId
                        where f.AccountId == account.Id
                        select new
                        {
                            f,
                            fd
                        };

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(r => r.fd.Status == status);
            }

            if (!string.IsNullOrEmpty(label))
            {
                query = query.Where(r => r.fd.Label == label);
            }

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(r => r.fd.Type == type);
            }

            var result = await query.ToListAsync();

            List<FormResponse> formResponses = result.Select(r => new FormResponse
            {
                Title = r.fd.Title,
                Description = r.fd.Description,
                Status = r.fd.Status,
                Label = r.fd.Label,
                Type = r.fd.Type,
                CreateAt = r.f.Timestamp
            }).ToList();

            return formResponses;
        }

    }
}