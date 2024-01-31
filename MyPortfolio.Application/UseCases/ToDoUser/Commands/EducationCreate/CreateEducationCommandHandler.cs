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

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.EducationCreate
{
    public sealed class CreateEducationCommandHandler : IRequestHandler<CreateEducationCommand, Education>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<CreateEducationCommandHandler> _logger;
        private readonly ICurrentUserService _currentUser;
        public CreateEducationCommandHandler(
            IAppDbContext context,
            ILogger<CreateEducationCommandHandler> logger,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _logger = logger;
            _currentUser = currentUserService;
        }
        public async Task<Education> Handle(CreateEducationCommand request, CancellationToken cancellationToken)
        {
            Education education = new(
                request.Name,
                request.Description,
                request.City,
                request.FromDate,
                request.ToDate,
                request.EducationWebSiteUrl,
                _currentUser.UserId);
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == _currentUser.UserId, cancellationToken) ?? throw new NotFoundException("User not found!");

                education.User = user;

                await _context.Educations.AddAsync(education, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An error occurred: {ex.Message}", ex.Message);
            }

            return education;
        }
    }
}
