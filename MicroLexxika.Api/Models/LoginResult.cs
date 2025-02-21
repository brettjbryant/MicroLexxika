namespace MicroLexxika.Api.Models
{
    public class LoginResult
    {
        public required string UserId { get; set; }
        public required string Token { get; set; }
        public required string Expiry { get; set; }
    }
}
