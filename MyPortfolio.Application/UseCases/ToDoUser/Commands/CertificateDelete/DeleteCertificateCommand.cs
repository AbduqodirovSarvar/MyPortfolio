using MediatR;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.CertificateDelete
{
    public sealed class DeleteCertificateCommand : IRequest<bool>
    {
        public DeleteCertificateCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
