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

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.ExperienceDelete
{
    public class DeleteExperienceCommandHandler : IRequestHandler<DeleteExperienceCommand, bool>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<DeleteExperienceCommandHandler> _logger;
        public DeleteExperienceCommandHandler(
            IAppDbContext context,
            ICurrentUserService currentUserService,
            ILogger<DeleteExperienceCommandHandler> logger
            )
        {
            _context = context;
            _currentUser = currentUserService;
            _logger = logger;
        }
        public async Task<bool> Handle(DeleteExperienceCommand request, CancellationToken cancellationToken)
        {
            var experience = await _context.Experiences
                                        .Where(x => x.UserId == _currentUser.UserId && x.Id == request.Id)
                                            .FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException("Certificate not found!");

            _context.Experiences.Remove(experience);

            bool result = (await _context.SaveChangesAsync(cancellationToken)) > 0;

            if (result)
            {
                _logger.LogInformation("Certificate (ID: {CertificateId}) removed by user (ID: {UserId})", experience.Id, _currentUser.UserId);
            }
            return result;
        }
    }
}
