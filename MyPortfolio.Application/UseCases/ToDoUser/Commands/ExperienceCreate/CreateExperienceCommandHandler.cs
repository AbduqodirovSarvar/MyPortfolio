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

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.ExperienceCreate
{
    public sealed class CreateExperienceCommandHandler : IRequestHandler<CreateExperienceCommand, ExperienceViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<CreateExperienceCommandHandler> _logger;
        private readonly IMapper _mapper;
        public CreateExperienceCommandHandler(
            IAppDbContext context,
            ICurrentUserService currentUserService,
            ILogger<CreateExperienceCommandHandler> logger,
            IMapper mapper)
        {
            _context = context;
            _currentUser = currentUserService;
            _mapper = mapper;
            _logger = logger;
        }

        async Task<ExperienceViewModel> IRequestHandler<CreateExperienceCommand, ExperienceViewModel>.Handle(CreateExperienceCommand request, CancellationToken cancellationToken)
        {
            Experience experience = new(
                request.CompanyName,
                request.Description,
                request.Position,
                (WorkType)Enum.Parse(typeof(WorkType), request.WorkType),
                request.City,
                request.FromDate,
                request.ToDate,
                _currentUser.UserId);
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == _currentUser.UserId, cancellationToken) ?? throw new NotFoundException("User not found!");

                experience.User = user;

                await _context.Experiences.AddAsync(experience, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An error occurred: {ex.Message}", ex.Message);
            }

            return _mapper.Map<ExperienceViewModel>(experience);
        }
    }
}
