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

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.CertificateCreate
{
    public class CreateCertificateCommandHandler : IRequestHandler<CreateCertificateCommand, Certificate>
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IAppDbContext _context;
        private readonly ILogger<CreateCertificateCommand> _logger;
        public CreateCertificateCommandHandler(
            ICurrentUserService currentUserService,
            IAppDbContext appDbContext,
            ILogger<CreateCertificateCommand> logger
            )
        {
            _context = appDbContext;
            _currentUser = currentUserService;
            _logger = logger;
        }
        public async Task<Certificate> Handle(CreateCertificateCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            Certificate certificate = new(
                request.Name,
                request.Description,
                request.CertificateUrl,
                request.Credential,
                request.Issued,
                userId);

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken) ?? throw new NotFoundException("User not found!");

                certificate.User = user;

                await _context.Certificates.AddAsync(certificate, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An error occurred: {ex.Message}", ex.Message);
            }

            return certificate;
        }
    }
}
