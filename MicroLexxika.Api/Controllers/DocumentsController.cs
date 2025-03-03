﻿using MicroLexxika.Api.Models;
using Microsoft.AspNetCore.Mvc;
using MicroLexxika.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<IActionResult> GetAsync(string documentId)
        {
            if (string.IsNullOrEmpty(documentId))
            {
                return BadRequest();
            }

            var documents = await _documentService.GetAsync(int.Parse(documentId));

            return Ok(documents);
        }

        [HttpGet]
        [Route("getbyuser/{userId}")]
        public async Task<IActionResult> GetByUserAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }

            var documents = await _documentService.GetByUserAsync(int.Parse(userId));

            return Ok(documents);
        }


        [HttpPost]
        [Route("create")]
        public IActionResult Create(DocumentRequest documentRequest)
        {
            if (documentRequest == null)
            {
                return BadRequest();
            }

            var document = _documentService.Create(documentRequest);

            return Ok(document);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update(DocumentRequest documentRequest)
        {
            if (documentRequest == null)
            {
                return BadRequest();
            }

            var document = _documentService.UpdateAsync(documentRequest);

            return Ok(document);
        }
    }
}
