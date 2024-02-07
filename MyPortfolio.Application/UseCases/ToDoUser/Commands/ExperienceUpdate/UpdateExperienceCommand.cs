using MediatR;
using MyPortfolio.Application.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.ExperienceUpdate
{
    public sealed class UpdateExperienceCommand : IRequest<ExperienceViewModel>
    {
        [Required]
        public long Id { get; set; }
        public string? CompanyName { get; set; } = null;
        public string? Description { get; set; } = null;
        public string? Position { get; set; } = null;
        public string? WorkType { get; set; } = null;
        public string? City { get; set; } = null;
        public DateOnly? FromDate { get; set; } = null;
        public DateOnly? ToDate { get; set; } = null;
        public ICollection<string> Skills { get; set; } = new List<string>();
    }
}
