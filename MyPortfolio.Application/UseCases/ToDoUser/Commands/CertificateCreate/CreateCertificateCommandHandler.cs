using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Entities;
using MyPortfolio.Entity.Exceptions;

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

                certificate.Skills = request.Skills
                                            .Select(skillName => _context.CertificateSkills.Add(new CertificateSkill(
                                                    _context.Skills.FirstOrDefault(x => x.Name == skillName)
                                                    ?? _context.Skills.Add(new Skill(skillName)).Entity, certificate)).Entity)
                                            .ToList();

                await _context.Certificates.AddAsync(certificate, cancellationToken);

                string resultMessage = (await _context.SaveChangesAsync(cancellationToken)) > 0
                                               ? "Certificate (ID: {Id}) created by user (ID: {_currentUser.UserId})"
                                               : "Certificate (ID: {Id}) couldn't create by user (ID: {_currentUser.UserId})";
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
