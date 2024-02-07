using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Entities;
using MyPortfolio.Entity.Exceptions;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.EducationCreate
{
    public sealed class CreateEducationCommandHandler : IRequestHandler<CreateEducationCommand, EducationViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<CreateEducationCommandHandler> _logger;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;
        public CreateEducationCommandHandler(
            IAppDbContext context,
            ILogger<CreateEducationCommandHandler> logger,
            ICurrentUserService currentUserService,
            IMapper mapper
            )
        {
            _context = context;
            _logger = logger;
            _currentUser = currentUserService;
            _mapper = mapper;
        }
        public async Task<EducationViewModel> Handle(CreateEducationCommand request, CancellationToken cancellationToken)
        {
            Education education = new(
                request.Name,
                request.Description,
                request.City,
                request.FromDate,
                request.ToDate,
                request.EducationWebSiteUrl,
                _currentUser.UserId);
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == _currentUser.UserId, cancellationToken)
                                        ?? throw new NotFoundException("User not found!");

                education.User = user;

                await _context.Educations.AddAsync(education, cancellationToken);

                string resultMessage = (await _context.SaveChangesAsync(cancellationToken)) > 0
                                               ? "Education (ID: {Id}) created by user (ID: {_currentUser.UserId})"
                                               : "Certificate (ID: {Id}) couldn't create by user (ID: {_currentUser.UserId})";

                _logger.LogInformation(resultMessage, education.Id, _currentUser.UserId);

                return _mapper.Map<EducationViewModel>(education);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An error occurred: {ex.Message}", ex.Message);
                throw new AlreadyExistsException(ex.Message, ex);
            }
        }
    }
}
