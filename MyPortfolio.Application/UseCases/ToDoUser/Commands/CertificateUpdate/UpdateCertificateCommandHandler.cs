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

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.CertificateUpdate
{
    public sealed class UpdateCertificateCommandHandler : IRequestHandler<UpdateCertificateCommand, CertificateViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<UpdateCertificateCommandHandler> _logger;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public UpdateCertificateCommandHandler(
            IAppDbContext context,
            ILogger<UpdateCertificateCommandHandler> logger,
            ICurrentUserService currentUserService,
            IMapper mapper
            )
        {
            _context = context;
            _logger = logger;
            _currentUser = currentUserService;
            _mapper = mapper;
        }
        public async Task<CertificateViewModel> Handle(UpdateCertificateCommand request, CancellationToken cancellationToken)
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

            certificate.Change(changedCertificate);

            string resultMessage = (await _context.SaveChangesAsync(cancellationToken)) > 0 ? "Certificate (ID: {certificate.Id}) updated by user (ID: {_currentUser.UserId})"
                                       : "Certificate (ID: {certificate.Id}) couldn't update by user (ID: {_currentUser.UserId})";

            _logger.LogInformation(resultMessage, certificate.Id, _currentUser.UserId);

            return _mapper.Map<CertificateViewModel>(certificate);
        }
    }
}
