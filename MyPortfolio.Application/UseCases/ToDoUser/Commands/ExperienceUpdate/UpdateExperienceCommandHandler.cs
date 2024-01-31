using AutoMapper;
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

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.ExperienceUpdate
{
    public sealed class UpdateExperienceCommandHandler : IRequestHandler<UpdateExperienceCommand, ExperienceViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateExperienceCommandHandler> _logger;

        public UpdateExperienceCommandHandler(
            IAppDbContext context,
            ICurrentUserService currentUser,
            IMapper mapper,
            ILogger<UpdateExperienceCommandHandler> logger)
        {
            _context = context;
            _currentUser = currentUser;
            _mapper = mapper;
            _logger = logger;
        }

        async Task<ExperienceViewModel> IRequestHandler<UpdateExperienceCommand, ExperienceViewModel>.Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
        {
            var experience = await _context.Experiences.Include(x => x.User)
                                           .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == _currentUser.UserId, cancellationToken);

            var changedExperience = experience == null ? throw new NotFoundException("Experience was not found")
                                                       : experience.User == null ? throw new NotFoundException("User was not found")
                                                                                 : new Experience(request.CompanyName ?? experience.CompanyName,
                                                                                                  request.Description ?? experience.Description,
                                                                                                  request.Position ?? experience.Position,
                                                                                                  request.WorkType != null ? (WorkType)Enum.Parse(typeof(WorkType), request.WorkType)
                                                                                                                           : experience.WorkType,
                                                                                                  request.City ?? experience.City,
                                                                                                  request.FromDate ?? experience.FromDate,
                                                                                                  request.ToDate ?? experience.ToDate,
                                                                                                  experience.UserId);

            _logger.LogInformation("Experience (ID: {experience.Id}) updated by user (ID: {_currentUser.UserId})", experience.Id, _currentUser.UserId);

            experience.Change(changedExperience);
            return _mapper.Map<ExperienceViewModel>(experience);
        }
    }
}
