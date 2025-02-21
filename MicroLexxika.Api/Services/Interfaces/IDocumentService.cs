using MicroLexxika.Api.Models;
using MicroLexxika.Domain.Models;

namespace MicroLexxika.Api.Services.Interfaces
{
    public interface IDocumentService
    {
        public Task<Document> GetAsync(int documentId);
        public Task<IList<Document>> GetByUserAsync(int userId);
        public IList<Document> GetAll();
        public Task<Document> UpdateAsync(DocumentRequest documentRequest);
        public Document Create(DocumentRequest documentRequest);
    }
}
