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

        public async Task<List<FormResponse>> HandleGetAllFormRequest(string accessToken, string? status, string? label, string? type)
        {
            try
            {
                Account account = await _authLibraryService.FetchAccount(accessToken);
                Common.Models.Role role = await _authorizeRepository.FetchRoleFromAccount(account);

                if (role.Name == Common.Enums.Role.Admin.ToString())
                {
                    List<FormResponse> formResponses = await _formRepository.GetAllFormRequest(status, label, type);
                    return formResponses;
                }
                else if (role.Name == Common.Enums.Role.User.ToString())
                {
                    List<FormResponse> formResponses = await _formRepository.GetAllFormRequestByAccount(account, status, label, type);
                    return formResponses;
                }
                else
                {
                    throw new Exception("Invalid Role");
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
                FormResidentRequestDetail formResidentRequestDetail = new()
                {
                    FormResidentRequestId = formResidentRequest.Id,
                    Title = request.Title,
                    Type = request.Type,
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