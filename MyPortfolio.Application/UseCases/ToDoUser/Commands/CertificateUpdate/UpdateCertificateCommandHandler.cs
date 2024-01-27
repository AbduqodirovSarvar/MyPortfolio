using MediatR;
using MyPortfolio.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.CertificateUpdate
{
    public sealed class UpdateCertificateCommandHandler : IRequestHandler<UpdateCertificateCommand, Certificate>
    {
        public Task<Certificate> Handle(UpdateCertificateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
