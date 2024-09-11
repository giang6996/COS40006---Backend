namespace Server.Common.DTOs
{
    public class FormResponse
    {
        public required long Id { get; set; }
        public string? ResidentName { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
        public required string Label { get; set; }
        public required string Type { get; set; }
        public DateTime CreateAt { get; set; }
    }
}