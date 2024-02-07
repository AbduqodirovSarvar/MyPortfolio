using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Entity.Exceptions;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.ExperienceDelete
{
    public sealed class DeleteExperienceCommandHandler : IRequestHandler<DeleteExperienceCommand, bool>
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
                                           .FirstOrDefaultAsync(x => x.UserId == _currentUser.UserId && x.Id == request.Id, cancellationToken)
                                           ?? throw new NotFoundException("Certificate not found!");

            _context.Experiences.Remove(experience);

            bool result = (await _context.SaveChangesAsync(cancellationToken)) > 0;
            string resultMessage = result
                                       ? "Experience (ID: {Id}) removed by user (ID: {UserId})"
                                       : "Experience (ID: {Id}) couldn't remove by user (ID: {UserId})";

            _logger.LogInformation(resultMessage, experience.Id, _currentUser.UserId);

            return result;
        }
    }
}
