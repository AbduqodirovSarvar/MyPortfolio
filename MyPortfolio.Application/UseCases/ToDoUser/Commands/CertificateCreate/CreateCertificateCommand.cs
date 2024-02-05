using MediatR;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Behaviour;
using MyPortfolio.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
