using MediatR;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.ExperienceDelete
{
    public sealed class DeleteExperienceCommand : IRequest<bool>
    {
        public DeleteExperienceCommand(long id)
        {
            Id = id;
        }
        public long Id { get; set; }
    }
}
