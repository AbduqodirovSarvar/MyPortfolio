using Microsoft.AspNetCore.Http;
using MyPortfolio.Application.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Services
{
    public class FileService : IFileService
    {
        public Task RemoveFileAsync(string? url)
        {
            return Task.CompletedTask;
        }

        public Task<Uri?> SaveFileAsync(IFormFile? uri)
        {
            return Task.FromResult<Uri?>(new Uri("http://job.sarvarbekabduqodirov.uz/swagger/index.html"));
        }
    }
}
