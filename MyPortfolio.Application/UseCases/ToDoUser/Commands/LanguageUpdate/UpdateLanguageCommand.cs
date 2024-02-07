using MediatR;
using MyPortfolio.Application.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.LanguageUpdate
{
    public sealed class UpdateLanguageCommand : IRequest<UserLanguageViewModel>
    {
        [Required]
        public long LanguageId { get; set; }
        public string? LanguageLevel { get; set; } = null;
    }
}
