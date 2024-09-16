using Server.Models.DTOs.Group;

namespace Server.BusinessLogic.Interfaces
{
    public interface IGroupService
    {
        Task HandleCreateGroup(string accessToken, CreateGroupRequest request);
        Task HandleAddAccounts(AddAccountToGroupRequest request);
    }
}