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
    public sealed class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<CreateProjectCommandHandler> _logger;
        private readonly IFileService _saveFileService;
        private readonly IMapper _mapper;
        public CreateProjectCommandHandler(
            IAppDbContext appDbContext,
            ICurrentUserService currentUserService,
            ILogger<CreateProjectCommandHandler> logger,
            IFileService saveFileService,
            IMapper mapper
            )
        {
            _context = appDbContext;
            _currentUser = currentUserService;
            _logger = logger;
            _saveFileService = saveFileService;
            _mapper = mapper;
        }
        async Task<ProjectViewModel> IRequestHandler<CreateProjectCommand, ProjectViewModel>.Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project(
                                request.Name,
                                request.Description,
                                (await _saveFileService.SaveFileAsync(request.Photo))?.ToString(),
                                _currentUser.UserId,
                                request.UrlToCode,
                                request.UrlToSite);

            project.Skills = (from skillName in request.Skills
                              let skill = _context.Skills.FirstOrDefault(x => x.Name == skillName) ?? _context.Skills.Add(new Skill(skillName)).Entity
                              select _context.ProjectSkills.Add(new ProjectSkill(skill, project)).Entity).ToList();

            string resultMessage = (await _context.SaveChangesAsync(cancellationToken)) > 0 ? "Project (ID: {Id}) created by user (ID: {_currentUser.UserId})"
                                       : "Project (ID: {Id}) couldn't create by user (ID: {_currentUser.UserId})";

            _logger.LogInformation(resultMessage, project.Id, _currentUser.UserId);

            return _mapper.Map<ProjectViewModel>(project);
        }
    }
}
