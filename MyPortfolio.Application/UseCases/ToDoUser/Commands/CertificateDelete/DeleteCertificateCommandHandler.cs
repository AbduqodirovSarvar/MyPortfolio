using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.CertificateDelete
{
    public sealed class DeleteCertificateCommandHandler : IRequestHandler<DeleteCertificateCommand, bool>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUser;
        public DeleteCertificateCommandHandler(
            IAppDbContext context,
            ILogger logger,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _logger = logger;
            _currentUser = currentUserService;
        }
        public async Task<bool> Handle(DeleteCertificateCommand request, CancellationToken cancellationToken)
        {
            var certificate = await _context.Certificates
                                        .Where(x => x.UserId == _currentUser.UserId && x.Id == request.Id)
                                            .FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException("Certificate not found!");

            _context.Certificates.Remove(certificate);

            bool result = (await _context.SaveChangesAsync(cancellationToken)) > 0;

            if (result)
            {
                _logger.LogInformation("Certificate (ID: {CertificateId}) removed by user (ID: {UserId})", certificate.Id, _currentUser.UserId);
            }

            return result;
        }
    }
}
