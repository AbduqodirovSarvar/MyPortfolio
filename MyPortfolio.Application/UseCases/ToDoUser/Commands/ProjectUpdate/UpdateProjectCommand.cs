using MediatR;
using Microsoft.AspNetCore.Http;
using MyPortfolio.Application.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.ProjectUpdate
{
    public sealed class UpdateProjectCommand : IRequest<ProjectViewModel>
    {
        [Required]
        public long Id { get; set; }
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;
        public IFormFile? Photo { get; set; } = null;
        public string? UrlToCode { get; set; } = null;
        public string? UrlToSite { get; set; } = null;
        public ICollection<string> Skills { get; set; } = new List<string>();
    }
}
