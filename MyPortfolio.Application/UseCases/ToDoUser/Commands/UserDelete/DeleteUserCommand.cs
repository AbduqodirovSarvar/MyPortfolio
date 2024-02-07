using MediatR;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.UserDelete
{
    public sealed class DeleteUserCommand : IRequest<bool>
    {
        public DeleteUserCommand(long id)
        {
            Id = id;
        }
        public long Id { get; set; }
    }
}
