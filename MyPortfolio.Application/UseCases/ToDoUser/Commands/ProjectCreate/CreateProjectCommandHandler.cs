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

            foreach (var skillName in request.Skills)
            {
                var skill = await _context.Skills.FirstOrDefaultAsync(x => x.Name == skillName, cancellationToken) ?? new Skill(skillName);
                if (skill == null)
                {
                    skill = new Skill(skillName);
                    await _context.Skills.AddAsync(skill, cancellationToken);
                }

                project.Skills.Add(new ProjectSkill(skill, project.Id));
            }

            string resultMessage = (await _context.SaveChangesAsync(cancellationToken)) > 0 ? "Project (ID: {Id}) created by user (ID: {_currentUser.UserId})"
                                       : "Project (ID: {Id}) couldn't create by user (ID: {_currentUser.UserId})";

            _logger.LogInformation(resultMessage, project.Id, _currentUser.UserId);

            return _mapper.Map<ProjectViewModel>(project);
        }
    }
}
