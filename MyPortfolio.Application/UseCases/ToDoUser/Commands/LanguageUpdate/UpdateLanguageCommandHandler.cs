using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Entities;
using MyPortfolio.Entity.Enums;
using MyPortfolio.Entity.Exceptions;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.LanguageUpdate
{
    public sealed class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, UserLanguageViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<UpdateLanguageCommandHandler> _logger;
        private readonly IMapper _mapper;
        public UpdateLanguageCommandHandler(
            IAppDbContext context,
            ICurrentUserService currentUser,
            ILogger<UpdateLanguageCommandHandler> logger,
            IMapper mapper
            )
        {
            _context = context;
            _currentUser = currentUser;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<UserLanguageViewModel> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            var userLanguage = await _context.UserLanguages.Include(x => x.User)
                                             .FirstOrDefaultAsync(x => x.LanguageId == request.LanguageId && x.UserId == _currentUser.UserId, cancellationToken);

            var changedUserLanguage = userLanguage == null ? throw new NotFoundException("Experience was not found")
                                                       : userLanguage.User == null ? throw new NotFoundException("User was not found")
                                                                                 : new UserLanguage(userLanguage.LanguageId,
                                                                                                    _currentUser.UserId,
                                                                                                    request.LanguageLevel != null ? (LanguageLevel)Enum.Parse(typeof(LanguageLevel), request.LanguageLevel)
                                                                                                                                : userLanguage.LanguageLevel);

            userLanguage.Change(changedUserLanguage);

            string resultMessage = (await _context.SaveChangesAsync(cancellationToken)) > 0 ? "Language (ID: {Id}) created by user (ID: {_currentUser.UserId})"
                                       : "Language (ID: {Id}) couldn't create by user (ID: {_currentUser.UserId})";

            _logger.LogInformation(resultMessage, userLanguage.Id, _currentUser.UserId);
            return _mapper.Map<UserLanguageViewModel>(userLanguage);
        }
    }
}
