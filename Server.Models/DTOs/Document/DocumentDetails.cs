namespace Server.Models.DTOs.Document
{
    public class DocumentDetails
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public string? DocumentDesc { get; set; }
        public required string Status { get; set; }
        public string? DocumentLink { get; set; }
    }
}