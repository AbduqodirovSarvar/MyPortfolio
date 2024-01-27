using MediatR;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
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
    public sealed class CreateCertificateCommand : IRequest<Certificate>
    {
        public CreateCertificateCommand(
            string name,
            string description,
            string certificateurl,
            string credential,
            DateOnly issued
            ) 
        {
            Name = name;
            Description = description;
            CertificateUrl = certificateurl;
            Credential = credential;
            Issued = issued;
        }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public string CertificateUrl { get; set; }
        public string Credential { get; set; }
        public DateOnly Issued { get; set; }
        public long UserId { get; set; }
    }
}
