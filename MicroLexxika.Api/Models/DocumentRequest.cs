namespace MicroLexxika.Api.Models
{
    public class DocumentRequest
    {
        public int? Id { get; set; }
        public required int UserId { get; set; }
        public required string Title { get; set; }
        public required string Body { get; set; }
    }
}
