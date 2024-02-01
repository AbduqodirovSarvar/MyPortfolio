﻿using Microsoft.AspNetCore.Http;
using MyPortfolio.Application.Abstractions.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Services
{
    public class FileService : IFileService
    {
        private readonly string _filesDirectory;

        public FileService()
        {
            // Define the base directory for storing files
            _filesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "MyPortfolio.Application", "Files");

            // Ensure the directory exists
            Directory.CreateDirectory(_filesDirectory);
        }

        public Task RemoveFileAsync(string? fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return Task.CompletedTask;
            }

            try
            {
                string filePath = Path.Combine(_filesDirectory, fileName);
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
            if (file == null)
            {
                return null;
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(_filesDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return new Uri("https://api.portfolio.sarvarbekabduqodirov.uz/api/storage/" + fileName);
        }

        public Stream? GetFileByFileName(string fileName)
        {
            try
            {
                string filePath = Path.Combine(_filesDirectory, fileName);
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

        public string GetFilePath(string fileName)
        {
            try
            {
                string filePath = Path.Combine(_filesDirectory, fileName);
                string fullPath = Path.GetFullPath(filePath);
                return fullPath;
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
