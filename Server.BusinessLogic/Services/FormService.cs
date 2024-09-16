using System.Security.Claims;
using Server.BusinessLogic.Interfaces;
using Server.Common.DTOs;
using Server.Common.Enums;
using Server.Common.Models;
using Server.DataAccess.Interfaces;

namespace Server.BusinessLogic.Services
{

    public class FormService : IFormService
    {
        private readonly IAuthLibraryService _authLibraryService;
        private readonly IFormRepository _formRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IAuthorizeRepository _authorizeRepository;

        public FormService(IAuthLibraryService authLibraryService, IFormRepository formRepository, IModuleRepository moduleRepository, IAuthorizeRepository authorizeRepository)
        {
            _authLibraryService = authLibraryService;
            _formRepository = formRepository;
            _moduleRepository = moduleRepository;
            _authorizeRepository = authorizeRepository;
        }

        public async Task HandleAdminResponse(string accessToken, long id, string response, string status)
        {
            try
            {
                Account account = await _authLibraryService.FetchAccount(accessToken);
                Common.Models.Role role = await _authorizeRepository.FetchRoleFromAccount(account);

                if (role.Name == Common.Enums.Role.Admin.ToString())
                {
                    if (Enum.TryParse(status, out FormStatus statusParsed))
                    {
                        await _formRepository.UpdateFormResponse(id, response, status);
                    }
                    else
                    {
                        throw new ArgumentException("Given status not valid");
                    }
                }
                else
                {
                    throw new Exception("You not have permission");
                }

            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<List<FormResponse>> HandleGetAllFormRequest(string accessToken, Common.Enums.Permission userPermission, string? status, string? label, string? type)
        {
            try
            {
                long accountId = (long)Convert.ToDouble(_authLibraryService.GetClaimValue(ClaimTypes.NameIdentifier, accessToken));

                if (userPermission == Common.Enums.Permission.CanViewAllForms)
                {
                    List<FormResponse> formResponses = await _formRepository.GetAllFormRequest(status, label, type);
                    return formResponses;
                }
                else if (userPermission == Common.Enums.Permission.CanViewTenantForms)
                {
                    List<FormResponse> formResponses = await _formRepository.GetAllTenantForms(accountId, status, label, type);
                    return formResponses;
                }
                else
                {
                    List<FormResponse> formResponses = await _formRepository.GetAllOwnForms(accountId, status, label, type);
                    return formResponses;
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task HandleNewRequest(Models.DTOs.Form.FormResidentRequest request, string accessToken)
        {
            try
            {
                Account account = await _authLibraryService.FetchAccount(accessToken);
                Common.Models.Module module = await _moduleRepository.GetModuleByModuleName(Common.Enums.Module.Form);

                FormResidentRequest formResidentRequest = new()
                {
                    AccountId = account.Id,
                    ModuleId = module.Id,
                    Timestamp = DateTime.Now
                };
                await _formRepository.AddNewFormAsync(formResidentRequest);

                bool parseLabelState = Enum.TryParse(request.Label, out FormLabel formLabel);
                bool parseTypeState = Enum.TryParse(request.Type, out FormType formType);
                if (!parseTypeState)
                    throw new Exception("Form Type not found");

                FormResidentRequestDetail formResidentRequestDetail = new()
                {
                    FormResidentRequestId = formResidentRequest.Id,
                    Title = request.Title,
                    Type = formType.ToString(),
                    Label = parseLabelState ? formLabel.ToString() : FormLabel.Other.ToString(),
                    Description = request.Description,
                    Status = FormStatus.Submitted.ToString(),
                    RequestMediaLink = request.RequestMediaLink
                };
                await _formRepository.AddNewFormDetailAsync(formResidentRequestDetail);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}