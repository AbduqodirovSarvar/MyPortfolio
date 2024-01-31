using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Abstractions.Interfaces
{
    public interface ISaveFileService
    {
        Task<Uri> SaveFileAsync(IFormFile uri);
    }
}
