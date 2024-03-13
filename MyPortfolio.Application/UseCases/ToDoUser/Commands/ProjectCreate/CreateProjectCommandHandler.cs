using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Application.Services;
using MyPortfolio.Entity.Entities;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.ProjectCreate
{
    public sealed class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<CreateProjectCommandHandler> _logger;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        public CreateProjectCommandHandler(
            IAppDbContext appDbContext,
            ICurrentUserService currentUserService,
            ILogger<CreateProjectCommandHandler> logger,
            IFileService FileService,
            IMapper mapper
            )
        {
            _context = appDbContext;
            _currentUser = currentUserService;
            _logger = logger;
            _fileService = FileService;
            _mapper = mapper;
        }
        async Task<ProjectViewModel> IRequestHandler<CreateProjectCommand, ProjectViewModel>.Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project(
                                request.Name,
                                request.Description,
                                (await _fileService.SaveFileAsync(request.Photo))?.ToString(),
                                _currentUser.UserId,
                                request.UrlToCode,
                                request.UrlToSite);

            foreach (var skill in request.Skills)
            {
                var existingSkill = await _context.Skills.FirstOrDefaultAsync(x => x.Name == skill.Name, cancellationToken)
                                                         ?? (await _context.Skills.AddAsync(new Skill(skill.Name)
                                                         {
                                                             PhotoUrl = await _fileService.SaveFileAsync(skill.Photo)
                                                         }, cancellationToken)).Entity;

                await _context.ProjectSkills.AddAsync(new ProjectSkill(existingSkill, project), cancellationToken);
            }

            


            string resultMessage = (await _context.SaveChangesAsync(cancellationToken)) > 0 ? "Project (ID: {Id}) created by user (ID: {_currentUser.UserId})"
                                       : "Project (ID: {Id}) couldn't create by user (ID: {_currentUser.UserId})";

            _logger.LogInformation(resultMessage, project.Id, _currentUser.UserId);

            return _mapper.Map<ProjectViewModel>(project);
        }
    }
}
