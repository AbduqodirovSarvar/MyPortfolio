using Microsoft.AspNetCore.Http;

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
