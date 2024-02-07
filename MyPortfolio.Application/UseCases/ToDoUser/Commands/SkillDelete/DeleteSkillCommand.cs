using MediatR;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.SkillDelete
{
    public sealed class DeleteSkillCommand : IRequest<bool>
    {
        public DeleteSkillCommand(long skillId)
        {
            Id = skillId;
        }
        public long Id { get; set; }
    }
}
