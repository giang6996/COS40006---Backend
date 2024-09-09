using Server.BusinessLogic.Interfaces;
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

        public FormService(IAuthLibraryService authLibraryService, IFormRepository formRepository, IModuleRepository moduleRepository)
        {
            _authLibraryService = authLibraryService;
            _formRepository = formRepository;
            _moduleRepository = moduleRepository;
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

                FormResidentRequestDetail formResidentRequestDetail = new()
                {
                    FormResidentRequestId = formResidentRequest.Id,
                    Title = request.Title,
                    Type = request.Type,
                    Label = request.Label == "" ? FormLabel.Other.ToString() : request.Label,
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