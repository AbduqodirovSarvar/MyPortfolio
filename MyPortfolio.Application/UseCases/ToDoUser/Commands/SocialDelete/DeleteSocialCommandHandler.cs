using AutoMapper;
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

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.SocialDelete
{
    public sealed class DeleteSocialCommandHandler : IRequestHandler<DeleteSocialCommand, bool>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<DeleteSocialCommandHandler> _logger;

        public DeleteSocialCommandHandler(
            IAppDbContext context, 
            ICurrentUserService currentUser,
            ILogger<DeleteSocialCommandHandler> logger)
        {
            _context = context;
            _currentUser = currentUser;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteSocialCommand request, CancellationToken cancellationToken)
        {
            var social = await _context.Socials.
                                        FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == _currentUser.UserId, cancellationToken)
                                        ?? throw new NotFoundException("Social not found for the user");

            _context.Socials.Remove(social);

            bool result = (await _context.SaveChangesAsync(cancellationToken)) > 0;

            string resultMessage = result ? "Social network (ID: {socialId}) removed by user (ID: {UserId})"
                                      : "Social network (ID: {socialID}) couldn't remove by user (ID: {UserId})";

            _logger.LogInformation(resultMessage, social.Id, _currentUser.UserId);

            return result;
        }
    }
}
