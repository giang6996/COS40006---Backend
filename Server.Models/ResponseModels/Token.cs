namespace Server.Models.ResponseModels
{
    public class Token
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}