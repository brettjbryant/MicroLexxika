namespace MicroLexxika.Domain.Models
{
    public class Document
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public required string Title { get; set; }
        public required string Body { get; set; }
    }
}
