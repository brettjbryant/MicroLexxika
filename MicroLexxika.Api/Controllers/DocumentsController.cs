using MicroLexxika.Api.Models;
using Microsoft.AspNetCore.Mvc;
using MicroLexxika.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace MicroLexxika.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/docs")]
    public class DocumentsController : Controller
    {
        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        [Route("get/{documentId}")]
        public async Task<IActionResult> GetAsync(int documentId)
        {
            var documents = await _documentService.GetAsync(documentId);

            return Ok(documents);
        }

        [HttpGet]
        [Route("getbyuser/{userId}")]
        public async Task<IActionResult> GetByUserAsync(int userId)
        {
            var documents = await _documentService.GetByUserAsync(userId);

            return Ok(documents);
        }


        [HttpPost]
        [Route("create")]
        public IActionResult Create(DocumentRequest documentRequest)
        {
            var document = _documentService.Create(documentRequest);

            return Ok(document);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update(DocumentRequest documentRequest)
        {
            var document = _documentService.UpdateAsync(documentRequest);

            return Ok(document);
        }
    }
}
