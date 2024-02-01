using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Abstractions.Interfaces
{
    public interface IFileService
    {
        Task<Uri?> SaveFileAsync(IFormFile? uri);
        Task RemoveFileAsync(string? url);
        Stream? GetFileByFileName(string fileName);
        public string GetFilePath(string fileName);
    }
}
