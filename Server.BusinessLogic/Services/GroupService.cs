using System.Security.Claims;
using Server.BusinessLogic.Interfaces;
using Server.Common.Models;
using Server.DataAccess.Interfaces;
using Server.Models.DTOs.Group;

namespace Server.BusinessLogic.Services
{
    public class GroupService : IGroupService
    {
        private readonly IAuthLibraryService _authLibraryService;
        private readonly IGroupRepository _groupRepository;
        private readonly IPermissionRepository _permissionRepository;

        public GroupService(IAuthLibraryService authLibraryService, IGroupRepository groupRepository, IPermissionRepository permissionRepository)
        {
            _authLibraryService = authLibraryService;
            _groupRepository = groupRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task HandleAddAccounts(AddAccountToGroupRequest request)
        {
            List<AccountGroup> accountGroups = new();
            request.AccountIds.ForEach(id =>
            {
                accountGroups.Add(new()
                {
                    AccountId = id,
                    GroupId = request.GroupId
                });
            });

            await _groupRepository.AddAccountGroups(accountGroups);
        }

        public async Task HandleCreateGroup(string accessToken, CreateGroupRequest request)
        {
            long accountId = (long)Convert.ToDouble(_authLibraryService.GetClaimValue(ClaimTypes.NameIdentifier, accessToken));

            if (!request.AccountIds.Contains(accountId))
            {
                request.AccountIds.Add(accountId);
            }

            Group group = await _groupRepository.CreateGroup(null);
            List<Common.Enums.Permission> permissions = new();
            try
            {
                permissions = request.GroupPermissions.Select(gp => (Common.Enums.Permission)Enum
                                                                        .Parse(typeof(Common.Enums.Permission), gp))
                                                        .ToList();

                List<Permission> permissionsFromDb = await _permissionRepository.FetchPermissions(permissions);

                List<GroupPermission> groupPermissions = new();
                permissionsFromDb.ForEach(p =>
                {
                    groupPermissions.Add(new()
                    {
                        GroupId = group.Id,
                        PermissionId = p.Id
                    });
                });

                List<AccountGroup> accountGroups = new();
                request.AccountIds.ForEach(id =>
                {
                    accountGroups.Add(new()
                    {
                        AccountId = id,
                        GroupId = group.Id
                    });
                });

                await _groupRepository.AddGroupPermissions(groupPermissions);
                await _groupRepository.AddAccountGroups(accountGroups);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}