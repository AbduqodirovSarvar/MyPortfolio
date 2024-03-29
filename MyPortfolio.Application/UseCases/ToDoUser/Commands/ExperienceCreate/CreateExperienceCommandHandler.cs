﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Entities;
using MyPortfolio.Entity.Enums;
using MyPortfolio.Entity.Exceptions;

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

                experience.Skills = request.Skills
                                            .Select(skillName => _context.ExperienceSkills.Add(new ExperienceSkill(
                                                    _context.Skills.FirstOrDefault(x => x.Name == skillName)
                                                    ?? _context.Skills.Add(new Skill(skillName)).Entity, experience)).Entity)
                                            .ToList();

                await _context.Experiences.AddAsync(experience, cancellationToken);

                string resultMessage = (await _context.SaveChangesAsync(cancellationToken)) > 0
                                               ? "Experience (ID: {Id}) created by user (ID: {_currentUser.UserId})"
                                               : "Experience (ID: {Id}) couldn't create by user (ID: {_currentUser.UserId})";

                _logger.LogInformation(resultMessage, experience.Id, _currentUser.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An error occurred: {ex.Message}", ex.Message);
            }

            return _mapper.Map<ExperienceViewModel>(experience);
        }
    }
}
