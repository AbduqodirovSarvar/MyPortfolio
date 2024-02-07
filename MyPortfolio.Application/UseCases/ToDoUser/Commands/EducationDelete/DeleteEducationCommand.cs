using MediatR;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.EducationDelete
{
    public sealed class DeleteEducationCommand : IRequest<bool>
    {
        public DeleteEducationCommand(long id)
        {
            Id = id;
        }
        public long Id { get; set; }
    }
}
