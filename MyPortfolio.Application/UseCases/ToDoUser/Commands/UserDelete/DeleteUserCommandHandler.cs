using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Entity.Exceptions;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.UserDelete
{
    public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<DeleteUserCommandHandler> _logger;
        public DeleteUserCommandHandler(IAppDbContext context, ICurrentUserService currentUser, ILogger<DeleteUserCommandHandler> logger)
        {
            _context = context;
            _currentUser = currentUser;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ?? throw new NotFoundException("User not found");

            _context.Users.Remove(user);

            bool result = (await _context.SaveChangesAsync(cancellationToken)) > 0;

            string resultMessage = result
                                      ? "User (ID: {deletedUserId}) removed by user (ID: {UserId})"
                                      : "User (ID: {deletedUserId}) couldn't remove by user (ID: {UserId})";

            _logger.LogInformation(resultMessage, user.Id, _currentUser.UserId);

            return result;
        }
    }
}
