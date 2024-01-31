using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Entities;
using MyPortfolio.Entity.Enums;
using MyPortfolio.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.EducationUpdate
{
    public sealed class UpdateEducationCommandHandler : IRequestHandler<UpdateEducationCommand, Education>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<UpdateEducationCommandHandler> _logger;
        private readonly ICurrentUserService _currentUser;
        public UpdateEducationCommandHandler(
            IAppDbContext context, 
            ILogger<UpdateEducationCommandHandler> logger, 
            ICurrentUserService currentUser)
        {
            _context = context;
            _logger = logger;
            _currentUser = currentUser;
        }

        public async Task<Education> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
        {
            var education = await _context.Educations.Include(x => x.User)
                                          .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == _currentUser.UserId, cancellationToken);

            var changedEducation = education == null ? throw new NotFoundException("Education was not found")
                                                     : education.User == null ? throw new NotFoundException("User was not found")
                                                                              : new Education(request.Name ?? education.Name,
                                                                                              request.Description ?? education.Description,
                                                                                              request.City ?? education.City,
                                                                                              request.FromDate ?? education.FromDate,
                                                                                              request.ToDate ?? education.ToDate,
                                                                                              request.EducationWebSiteUrl ?? education.EducationWebSiteUrl,
                                                                                              education.UserId);

            _logger.LogInformation("Education (ID: {education.Id}) updated by user (ID: {_currentUser.UserId})", education.Id, _currentUser.UserId);

            education.Change(changedEducation);
            return education;
        }
    }
}
