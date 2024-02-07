using MediatR;
using Microsoft.AspNetCore.Http;
using MyPortfolio.Application.Models.ViewModels;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.ProjectCreate
{
    public sealed class CreateProjectCommand : IRequest<ProjectViewModel>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IFormFile Photo { get; set; } = null!;
        public string UrlToCode { get; set; } = null!;
        public string UrlToSite { get; set; } = null!;
        public ICollection<string> Skills { get; set; } = new List<string>();
    }
}
