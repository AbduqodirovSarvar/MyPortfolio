﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Entities;
using MyPortfolio.Entity.Exceptions;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.ProjectUpdate
{
    public sealed class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ProjectViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IFileService _fileService;
        private readonly ILogger<UpdateProjectCommandHandler> _logger;
        private readonly IMapper _mapper;
        public UpdateProjectCommandHandler(
            IAppDbContext context,
            ICurrentUserService currentUser,
            IFileService fileService,
            IMapper mapper,
            ILogger<UpdateProjectCommandHandler> logger
            )
        {
            _context = context;
            _currentUser = currentUser;
            _fileService = fileService;
            _mapper = mapper;
            _logger = logger;
        }

        async Task<ProjectViewModel> IRequestHandler<UpdateProjectCommand, ProjectViewModel>.Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.Include(x => x.Skills)
                                        .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == _currentUser.UserId, cancellationToken)
                                        ?? throw new NotFoundException("Project was not found");

            var changedProject = new Project(
                request.Name ?? project.Name,
                request.Description ?? project.Description,
                (await _fileService.SaveFileAsync(request.Photo))?.ToString() ?? project.PhotoUrl,
                _currentUser.UserId,
                request.UrlToCode ?? project.UrlToCode,
                request.UrlToSite ?? project.UrlToSite);

            if (request.Photo != null)
            {
                await _fileService.RemoveFileAsync(project.PhotoUrl);
            }

            project.Change(changedProject);

            project.Skills = request.Skills
                                        .Select(skillName =>
                                            project.Skills.FirstOrDefault(x => x.Skill?.Name == skillName)
                                            ?? _context.ProjectSkills.Add(new ProjectSkill(
                                                _context.Skills.FirstOrDefault(x => x.Name == skillName)
                                                ?? _context.Skills.Add(new Skill(skillName)).Entity, project)).Entity)
                                        .ToList();

            string resultMessage = (await _context.SaveChangesAsync(cancellationToken)) > 0
                                        ? "Project (ID: {Id}) created by user (ID: {_currentUser.UserId})"
                                        : "Project (ID: {Id}) couldn't create by user (ID: {_currentUser.UserId})";

            _logger.LogInformation(resultMessage, project.Id, _currentUser.UserId);

            return _mapper.Map<ProjectViewModel>(project);
        }
    }
}
