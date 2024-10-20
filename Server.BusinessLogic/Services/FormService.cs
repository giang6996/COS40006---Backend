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

        public async Task HandleAdminResponse(long id, string response, string status)
        {
            try
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
        public async Task<List<FormResponse>> HandleGetUserComplaints(string accessToken, string? status, string? label, string? type)
        {
            try
            {
                // Extract accountId from the access token
                long accountId = (long)Convert.ToDouble(_authLibraryService.GetClaimValue(ClaimTypes.NameIdentifier, accessToken));

                // Call the repository method to fetch only the user's complaints
                List<FormResponse> formResponses = await _formRepository.GetAllOwnForms(accountId, status, label, type);

                return formResponses;
            }
            catch (System.Exception ex)
            {
                throw new Exception("Failed to fetch user complaints", ex);
            }
        }

        public async Task<FormResponse> GetRequestDetail(long id)
        {
            try
            {
                // Call the repository method to fetch the form request details
                FormResponse formResponse = await _formRepository.GetFormRequestDetail(id);
                return formResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving form request details", ex);
            }
        }

        public async Task HandleNewRequest(Models.DTOs.Form.FormResidentRequest request, string accessToken)
        {
            try
            {
                // Extract the accountId from the access token
                long accountId = (long)Convert.ToDouble(_authLibraryService.GetClaimValue(ClaimTypes.NameIdentifier, accessToken));

                // Get the module associated with Form
                Common.Models.Module module = await _moduleRepository.GetModuleByModuleName(Common.Enums.Module.Form);

                // Create a new FormResidentRequest object
                FormResidentRequest formResidentRequest = new()
                {
                    AccountId = accountId,
                    ModuleId = module.Id,
                    Timestamp = DateTime.Now
                };

                // Save the new form request (without details)
                await _formRepository.AddNewFormAsync(formResidentRequest);

                // Create the FormResidentRequestDetail with a default "In-Process" status
                FormResidentRequestDetail formResidentRequestDetail = new()
                {
                    FormResidentRequestId = formResidentRequest.Id,
                    Title = request.Title,
                    Type = Enum.TryParse(request.Type, out FormType formType) ? formType.ToString() : throw new Exception("Invalid Form Type"),
                    Label = Enum.TryParse(request.Label, out FormLabel formLabel) ? formLabel.ToString() : FormLabel.Other.ToString(),
                    Description = request.Description,
                    Status = FormStatus.Await.ToString(), // Automatically set to "Await"
                    RequestMediaLink = request.RequestMediaLink
                };

                // Save the form details
                await _formRepository.AddNewFormDetailAsync(formResidentRequestDetail);
            }
            catch (System.Exception ex)
            {
                throw new Exception("Error creating new request", ex);
            }
        }

    }
}