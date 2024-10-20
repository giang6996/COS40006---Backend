using Microsoft.EntityFrameworkCore;
using Server.Common.DTOs;
using Server.Common.Models;
using Server.DataAccess.Interfaces;

namespace Server.DataAccess.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly AppDbContext _db;
        private readonly IAccountRepository _accountRepository;

        public FormRepository(AppDbContext db, IAccountRepository accountRepository)
        {
            _db = db;
            _accountRepository = accountRepository;
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
                Id = r.f.Id,
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

        public async Task<List<FormResponse>> GetAllOwnForms(long accountId, string? status, string? label, string? type)
        {
            var query = from f in _db.FormResidentRequests
                        join fd in _db.FormResidentRequestDetails on f.Id equals fd.FormResidentRequestId
                        where f.AccountId == accountId
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
                Id = r.f.Id,
                Title = r.fd.Title,
                Description = r.fd.Description,
                Status = r.fd.Status,
                Label = r.fd.Label,
                Type = r.fd.Type,
                CreateAt = r.f.Timestamp
            }).ToList();

            return formResponses;
        }

        public async Task<List<FormResponse>> GetAllTenantForms(long accountId, string? status, string? label, string? type)
        {
            var account = await _accountRepository.GetAccountByAccountIdAsync(accountId);
            long tenantId = account.TenantId;

            var query = from f in _db.FormResidentRequests
                        join fd in _db.FormResidentRequestDetails on f.Id equals fd.FormResidentRequestId
                        where f.Account.TenantId == tenantId
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
                Id = r.f.Id,
                Title = r.fd.Title,
                Description = r.fd.Description,
                Status = r.fd.Status,
                Label = r.fd.Label,
                Type = r.fd.Type,
                CreateAt = r.f.Timestamp
            }).ToList();

            return formResponses;
        }
        public async Task<FormResponse> GetFormRequestDetail(long id)
        {
            var result = await (
                from f in _db.FormResidentRequests
                join fd in _db.FormResidentRequestDetails on f.Id equals fd.FormResidentRequestId
                join a in _db.Accounts on f.AccountId equals a.Id
                where f.Id == id
                select new { f, fd, a }
            ).FirstOrDefaultAsync();

            if (result == null)
            {
                throw new Exception("Complaint not found");
            }

            // Map the result to FormResponse
            FormResponse formResponse = new FormResponse
            {
                Id = result.f.Id,
                ResidentName = $"{result.a.FirstName} {result.a.LastName}",
                Title = result.fd.Title,
                Description = result.fd.Description,
                Status = result.fd.Status,
                Label = result.fd.Label,
                Type = result.fd.Type,
                CreateAt = result.f.Timestamp,
                Response = result.fd.Response
            };

            return formResponse;
        }


        public async Task UpdateFormResponse(long id, string response, string status)
        {
            FormResidentRequestDetail formResidentRequestDetail = await _db.FormResidentRequestDetails.Where(fd => fd.FormResidentRequestId == id).FirstAsync()
            ?? throw new Exception("Update Form not found");

            formResidentRequestDetail.Response = response;
            formResidentRequestDetail.Status = status;

            _db.FormResidentRequestDetails.Update(formResidentRequestDetail);
            await _db.SaveChangesAsync();
        }
    }
}