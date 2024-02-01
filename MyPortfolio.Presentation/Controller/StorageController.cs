﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Application.Abstractions.Interfaces;
using System.IO;
using System.Net;
using System.Net.Mime;

namespace MyPortfolio.Presentation.Controller
{
    [Route("api/storage")]
    [ApiController]
    public class StorageController : ApiController
    {
        private readonly IFileService _fileService;

        public StorageController(IMediator mediator, IFileService fileService) : base(mediator)
        {
            _fileService = fileService;
        }

        [HttpGet("{fileName}")]
        public IActionResult Get([FromRoute] string fileName)
        {
            var fileStream = _fileService.GetFileByFileName(fileName);

            if (fileStream == null)
            {
                return NotFound();
            }

            string contentType = GetContentType(fileName);

            return File(fileStream, contentType, fileName);
        }

        private static string GetContentType(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLowerInvariant();

            return extension switch
            {
                ".png" => "image/png",
                ".jpg" or ".jpeg" => MediaTypeNames.Image.Jpeg,
                ".pdf" => MediaTypeNames.Application.Pdf,
                _ => MediaTypeNames.Application.Octet,
            };
        }
    }
}
