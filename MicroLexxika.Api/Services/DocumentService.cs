using MicroLexxika.Api.Models;
using MicroLexxika.Api.Services.Interfaces;
using MicroLexxika.Data;
using MicroLexxika.Domain.Models;

namespace MicroLexxika.Api.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly AppDbContext _dbContext;
        private readonly IUserService _userService;
        public DocumentService(AppDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<Document?> GetAsync(int documentId)
        {
            var document = await _dbContext.Documents.FindAsync(documentId);

            return document;
        }

        public async Task<IList<Document>> GetByUserAsync(int userId)
        {
            var user = await _userService.GetAsync(userId);
            var documents = new List<Document>();

            if (user != null)
            {
                documents.AddRange((user.Role == Role.Admin) ? _dbContext.Documents :
                    _dbContext.Documents.Where(x => x.UserId == userId));
            }

            return documents;
        }

        public IList<Document> GetAll()
        {
            return _dbContext.Documents.ToList();
        }

        public Document Create(DocumentRequest documentRequest)
        {
            var document = new Document
            {
                UserId = documentRequest.UserId,
                Title = documentRequest.Title,
                Body = documentRequest.Body
            };

            _dbContext.Documents.Add(document);
            _dbContext.SaveChanges();

            return document;
        }

        public async Task<Document?> UpdateAsync(DocumentRequest documentRequest)
        {
            var existingDocument = await GetAsync((int)documentRequest.Id);

            if (existingDocument != null)
            {
                existingDocument!.Title = documentRequest.Title;
                existingDocument!.Body = documentRequest.Body;

                _dbContext.SaveChanges();
            }

            return existingDocument;
        }
    }
}