namespace MicroLexxika.Api.Models
{
    public class UserRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required int Role { get; set; }
    }
}
