using MediatR;
using MyPortfolio.Application.Models.ViewModels;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.SkillCreate
{
    public sealed class CreateSkillCommand : IRequest<List<SkillViewModel>>
    {
        public List<string> Names { get; set; } = new List<string>();
    }
}
