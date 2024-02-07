using MediatR;
using MyPortfolio.Application.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.CertificateUpdate
{
    public sealed class UpdateCertificateCommand : IRequest<CertificateViewModel>
    {
        [Required]
        public long Id { get; set; }
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;
        public string? CertificateUrl { get; set; } = null;
        public string? Credential { get; set; } = null;
        public DateOnly? Issued { get; set; } = null;
        public ICollection<string> Skills { get; set; } = new List<string>();
    }
}
