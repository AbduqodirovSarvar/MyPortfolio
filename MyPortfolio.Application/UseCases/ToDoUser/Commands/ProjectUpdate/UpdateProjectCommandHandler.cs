using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Entities;
using MyPortfolio.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.ProjectUpdate
{
    public sealed class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Project>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IFileService _fileService;

        public UpdateProjectCommandHandler(
            IAppDbContext context,
            ICurrentUserService currentUser,
            IFileService fileService)
        {
            _context = context;
            _currentUser = currentUser;
            _fileService = fileService;
        }

        async Task<Project> IRequestHandler<UpdateProjectCommand, Project>.Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
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

            project.Skills = (from skillName in request.Skills
                           let skill = _context.Skills.FirstOrDefault(x => x.Name == skillName) ?? new Skill(skillName)
                           select new ProjectSkill(skill, project)).ToList();

            project.Change(changedProject);

            await _context.SaveChangesAsync(cancellationToken);

            return project;
        }
    }
}
