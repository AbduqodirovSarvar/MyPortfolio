using MediatR;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.LanguageDelete
{
    public sealed class DeleteLanguageCommand : IRequest<bool>
    {
        public DeleteLanguageCommand(long id)
        {
            Id = id;
        }
        public long Id { get; set; }
    }
}
