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
        public Task RemoveFileAsync(string? fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return Task.CompletedTask;
            }

            try
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "MyPortfolio.Application", "Files", fileName);
                string fullPath = Path.GetFullPath(filePath);

                // Check if the file exists before attempting to delete
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed (e.g., log or throw)
                Console.WriteLine($"Error removing file: {ex.Message}");
                throw;
            }
        }

        public async Task<Uri?> SaveFileAsync(IFormFile? file)
        {
            if(file == null)
            {
                return null;
            }
            string folderPath = Directory.GetCurrentDirectory();
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(folderPath, "..", "MyPortfolio.Application", "Files", fileName);
            string fp = Path.GetFullPath(filePath);

            using (var stream = new FileStream(fp, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return new Uri("https://api.portfolio.sarvarbekabduqodirov.uz/api/storage/" + fileName);
        }

        public Stream? GetFileByFileName(string fileName)
        {
            try
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "MyPortfolio.Application", "Files", fileName);
                string fullPath = Path.GetFullPath(filePath);

                if (File.Exists(fullPath))
                {
                    return new FileStream(fullPath, FileMode.Open, FileAccess.Read);
                }

                return null;
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed (e.g., log or throw)
                Console.WriteLine($"Error retrieving file: {ex.Message}");
                throw;
            }
        }
    }
}
