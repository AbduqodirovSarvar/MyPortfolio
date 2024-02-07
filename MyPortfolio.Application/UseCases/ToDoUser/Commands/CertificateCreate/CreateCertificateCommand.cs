using MediatR;
using MyPortfolio.Application.Models.ViewModels;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.CertificateCreate
{
    public sealed class CreateCertificateCommand : IRequest<CertificateViewModel>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CertificateUrl { get; set; } = null!;
        public string Credential { get; set; } = null!;
        public DateOnly Issued { get; set; }
        public ICollection<string> Skills { get; set; } = new List<string>();
    }
}
