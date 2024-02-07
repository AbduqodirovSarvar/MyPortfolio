using MediatR;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.ProjectDelete
{
    public sealed class DeleteProjectCommand : IRequest<bool>
    {
        public DeleteProjectCommand(long id)
        {
            Id = id;
        }
        public long Id { get; set; }
    }
}
