using MediatR;
using MyPortfolio.Application.Models.ViewModels;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.LanguageCreate
{
    public sealed class CreateLanguageCommand : IRequest<UserLanguageViewModel>
    {
        public string Name { get; set; } = null!;
        public string LanguageLevel { get; set; } = null!;
    }
}
