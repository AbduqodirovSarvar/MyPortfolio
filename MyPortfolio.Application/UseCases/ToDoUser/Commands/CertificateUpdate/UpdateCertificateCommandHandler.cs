using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Entity.Entities;
using MyPortfolio.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.CertificateUpdate
{
    public sealed class UpdateCertificateCommandHandler : IRequestHandler<UpdateCertificateCommand, Certificate>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<UpdateCertificateCommandHandler> _logger;
        private readonly ICurrentUserService _currentUser;

        public UpdateCertificateCommandHandler(
            IAppDbContext context,
            ILogger<UpdateCertificateCommandHandler> logger,
            ICurrentUserService currentUserService
            )
        {
            _context = context;
            _logger = logger;
            _currentUser = currentUserService;
        }
        public async Task<Certificate> Handle(UpdateCertificateCommand request, CancellationToken cancellationToken)
        {
            var certificate = await _context.Certificates.Include(x => x.User)
                                            .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == _currentUser.UserId, cancellationToken);

            var changedCertificate = (certificate==null) ? throw new NotFoundException("Certificate was not found")
                                                         : (certificate.User==null) ? throw new NotFoundException("User was not found")
                                                                                    : new Certificate(request.Name ?? certificate.Name,
                                                                                                      request.Description ?? certificate.Description,
                                                                                                      request.CertificateUrl ?? certificate.CertificateUrl,
                                                                                                      request.Credential ?? certificate.Credential,
                                                                                                      request.Issued ?? certificate.Issued,
                                                                                                      certificate.UserId);

            _logger.LogInformation("Certificate (ID: {certificate.Id}) updated by user (ID: {_currentUser.UserId})", certificate.Id, _currentUser.UserId);

            certificate.Change(changedCertificate);

            return certificate;
        }
    }
}
