using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Entities;
using MyPortfolio.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.CertificateCreate
{
    public sealed class CreateCertificateCommandHandler : IRequestHandler<CreateCertificateCommand, CertificateViewModel>
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IAppDbContext _context;
        private readonly ILogger<CreateCertificateCommand> _logger;
        private readonly IMapper _mapper;
        public CreateCertificateCommandHandler(
            ICurrentUserService currentUserService,
            IAppDbContext appDbContext,
            ILogger<CreateCertificateCommand> logger,
            IMapper mapper
            )
        {
            _context = appDbContext;
            _currentUser = currentUserService;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<CertificateViewModel> Handle(CreateCertificateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Certificate certificate = (await _context.Users.Include(x => x.Certificates)
                                .FirstOrDefaultAsync(x => x.Id == _currentUser.UserId, cancellationToken))?
                                    .Certificates.FirstOrDefault(x => x.Credential == request.Credential)
                                        ?? new(
                                                request.Name,
                                                request.Description,
                                                request.CertificateUrl,
                                                request.Credential,
                                                request.Issued,
                                                _currentUser.UserId);

                await _context.Certificates.AddAsync(certificate, cancellationToken);

                string resultMessage = (await _context.SaveChangesAsync(cancellationToken)) > 0 ? "Certificate (ID: {certificate.Id}) created by user (ID: {_currentUser.UserId})"
                                       : "Certificate (ID: {certificate.Id}) couldn't create by user (ID: {_currentUser.UserId})";
                return _mapper.Map<CertificateViewModel>(certificate);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An error occurred: {ex.Message}", ex.Message);
                throw new AlreadyExistsException("Certificate may already exists", ex);
            }
        }
    }
}
