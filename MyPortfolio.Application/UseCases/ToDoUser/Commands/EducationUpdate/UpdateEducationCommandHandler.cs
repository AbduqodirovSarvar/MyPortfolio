using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Entities;
using MyPortfolio.Entity.Exceptions;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.EducationUpdate
{
    public sealed class UpdateEducationCommandHandler : IRequestHandler<UpdateEducationCommand, EducationViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<UpdateEducationCommandHandler> _logger;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;
        public UpdateEducationCommandHandler(
            IAppDbContext context,
            ILogger<UpdateEducationCommandHandler> logger,
            ICurrentUserService currentUser,
            IMapper mapper
            )
        {
            _context = context;
            _logger = logger;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<EducationViewModel> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
        {
            var education = await _context.Educations.Include(x => x.User)
                                          .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == _currentUser.UserId, cancellationToken);

            var changedEducation = education == null ? throw new NotFoundException("Education was not found")
                                                     : education.User == null ? throw new NotFoundException("User was not found")
                                                                              : new Education(request.Name ?? education.Name,
                                                                                              request.Description ?? education.Description,
                                                                                              request.City ?? education.City,
                                                                                              request.FromDate ?? education.FromDate,
                                                                                              request.ToDate ?? education.ToDate,
                                                                                              request.EducationWebSiteUrl ?? education.EducationWebSiteUrl,
                                                                                              education.UserId);
            education.Change(changedEducation);

            string resultMessage = (await _context.SaveChangesAsync(cancellationToken)) > 0
                                           ? "Education (ID: {Id}) updated by user (ID: {_currentUser.UserId})"
                                           : "Education (ID: {Id}) couldn't update by user (ID: {_currentUser.UserId})";

            _logger.LogInformation(resultMessage, education.Id, _currentUser.UserId);

            return _mapper.Map<EducationViewModel>(education);
        }
    }
}
