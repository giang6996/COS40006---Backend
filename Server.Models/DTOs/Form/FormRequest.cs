namespace Server.Models.DTOs.Form
{
    public class FormResidentRequest
    {
        public required string Title { get; set; }
        public required string Type { get; set; }
        public required string Label { get; set; }
        public required string Description { get; set; }
        public string? RequestMediaLink { get; set; }
    }
}