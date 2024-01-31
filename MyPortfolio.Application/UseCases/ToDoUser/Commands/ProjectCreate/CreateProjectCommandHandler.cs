using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.ProjectCreate
{
    public sealed class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Project>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProjectCommandHandler> _logger;
        private readonly IFileService _saveFileService;
        public CreateProjectCommandHandler(
            IAppDbContext appDbContext,
            ICurrentUserService currentUserService,
            IMapper mapper,
            ILogger<CreateProjectCommandHandler> logger,
            IFileService saveFileService
            )
        {
            _context = appDbContext;
            _currentUser = currentUserService;
            _mapper = mapper;
            _logger = logger;
            _saveFileService = saveFileService;
        }
        async Task<Project> IRequestHandler<CreateProjectCommand, Project>.Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project(
                                request.Name,
                                request.Description,
                                (await _saveFileService.SaveFileAsync(request.Photo))?.ToString(),
                                _currentUser.UserId,
                                request.UrlToCode,
                                request.UrlToSite);

            foreach(var skillName in request.Skills)
            {
                var skill = await _context.Skills.FirstOrDefaultAsync(x => x.Name == skillName, cancellationToken);
                if (skill == null)
                {
                    skill = new Skill(skillName);
                    await _context.Skills.AddAsync(skill, cancellationToken);
                }

                project.Skills.Add(new ProjectSkill(skill, project.Id));
            }

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Project created by user (ID: {_currentUser.UserId})", _currentUser.UserId);

            return project;
        }
    }
}
