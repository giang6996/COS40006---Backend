namespace Server.Models.DTOs.Group
{
    public class CreateGroupRequest
    {
        public List<string> GroupPermissions { get; set; } = new();
        public List<long> AccountIds { get; set; } = new();
    }
}