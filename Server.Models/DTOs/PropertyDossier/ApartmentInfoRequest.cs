namespace Server.Models.DTOs.PropertyDossier
{
    public class ApartmentInfoRequest
    {
        public int RoomNumber { get; set; }
        public required string BuildingName { get; set; }
        public required string BuildingAddress { get; set; }
    }

}