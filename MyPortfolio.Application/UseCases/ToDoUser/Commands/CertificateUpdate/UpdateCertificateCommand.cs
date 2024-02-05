using MediatR;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Behaviour;
using MyPortfolio.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
