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

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.ProjectDelete
{
    public sealed class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, bool>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<DeleteProjectCommandHandler> _logger;
        public DeleteProjectCommandHandler(
            IAppDbContext context,
            ICurrentUserService currentUser,
            ILogger<DeleteProjectCommandHandler> logger)
        {
            _context = context;
            _currentUser = currentUser;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects
                                        .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == _currentUser.UserId, cancellationToken)
                                        ?? throw new NotFoundException("Language not found!");

            _context.Projects.Remove(project);

            bool result = (await _context.SaveChangesAsync(cancellationToken)) > 0;

            string resultMessage = result ? "Project (ID: {ProjectId}) removed by user (ID: {UserId})"
                                          : "Project (ID: {ProjectId}) couldn't remove by user (ID: {UserId})";

            _logger.LogInformation(resultMessage, project.Id, _currentUser.UserId);

            return result;
        }
    }
}
