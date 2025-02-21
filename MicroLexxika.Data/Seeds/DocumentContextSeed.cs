using MicroLexxika.Domain.Models;

namespace MicroLexxika.Data
{
    public static class DocumentContextSeed
    {
        public static void Seed(AppDbContext dbContext, IList<User> users)
        {
            if (!dbContext.Documents.Any())
            {
                var documents = new List<Document>();

                foreach (var user in users)
                {
                    Random random = new Random();
                    int randomNumber = random.Next(1, 10);

                    for (var i = 0; i < randomNumber; i++)
                    {
                        documents.Add(new Document
                        {
                            UserId = user.Id,
                            Title = $"Title {i} - Authored by: {user.Username}",
                            Body = $"Body {i}"
                        });
                    }
                }

                dbContext.Documents.AddRange(documents);
                dbContext.SaveChanges();
            }
        }
    }
}
