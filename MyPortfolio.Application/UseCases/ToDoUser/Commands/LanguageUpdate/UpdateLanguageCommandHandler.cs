using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.ExperienceUpdate;
using MyPortfolio.Entity.Entities;
using MyPortfolio.Entity.Enums;
using MyPortfolio.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.LanguageUpdate
{
    public sealed class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, UserLanguage>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<UpdateLanguageCommandHandler> _logger;

        public UpdateLanguageCommandHandler(
            IAppDbContext context,
            ICurrentUserService currentUser,
            ILogger<UpdateLanguageCommandHandler> logger)
        {
            _context = context;
            _currentUser = currentUser;
            _logger = logger;
        }
        public async Task<UserLanguage> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            var userLanguage = await _context.UserLanguages.Include(x => x.User)
                                             .FirstOrDefaultAsync(x => x.LanguageId == request.LanguageId && x.UserId == _currentUser.UserId, cancellationToken);

            var changedUserLanguage = userLanguage == null ? throw new NotFoundException("Experience was not found")
                                                       : userLanguage.User == null ? throw new NotFoundException("User was not found")
                                                                                 : new UserLanguage(userLanguage.LanguageId,
                                                                                                    _currentUser.UserId,
                                                                                                    request.LanguageLevel != null ? (LanguageLevel)Enum.Parse(typeof(LanguageLevel), request.LanguageLevel)
                                                                                                                                : userLanguage.LanguageLevel);

            _logger.LogInformation("Language (ID: {Language.Id}) updated by user (ID: {_currentUser.UserId})", userLanguage.LanguageId, _currentUser.UserId);

            userLanguage.Change(changedUserLanguage);
            return userLanguage;
        }
    }
}
