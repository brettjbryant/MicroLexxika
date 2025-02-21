namespace MicroLexxika.Domain.Models
{
    public class Token
    {
        public required string Value { get; set; }
        public required string Expires { get; set; }
    }
}
