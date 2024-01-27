using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
