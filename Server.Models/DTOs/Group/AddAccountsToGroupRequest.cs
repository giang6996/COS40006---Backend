namespace Server.Models.DTOs.Group
{
    public class AddAccountToGroupRequest
    {
        public long GroupId { get; set; } = new();
        public List<long> AccountIds { get; set; } = new();
    }
}