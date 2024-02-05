using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Entities;
using MyPortfolio.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.LanguageCreate
{
    public sealed class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, UserLanguageViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<CreateLanguageCommandHandler> _logger;
        private readonly IMapper _mapper;
        public CreateLanguageCommandHandler(
            IAppDbContext appDbContext,
            ICurrentUserService currentUserService,
            ILogger<CreateLanguageCommandHandler> logger,
            IMapper mapper
            )
        {
            _context = appDbContext;
            _currentUser = currentUserService;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<UserLanguageViewModel> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var language = await _context.Languages
                                            .FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken)
                                            ?? (await _context.Languages.AddAsync(new Language(request.Name), cancellationToken)).Entity;

                var userlanguage = new UserLanguage(
                    language,
                    _currentUser.UserId,
                    (LanguageLevel)Enum.Parse(typeof(LanguageLevel), request.LanguageLevel)
                    );

                userlanguage = (await _context.UserLanguages.AddAsync(userlanguage, cancellationToken)).Entity;

                string resultMessage = (await _context.SaveChangesAsync(cancellationToken)) > 0 ? "Language (ID: {Id}) created by user (ID: {_currentUser.UserId})"
                                       : "Language (ID: {Id}) couldn't create by user (ID: {_currentUser.UserId})";

                _logger.LogInformation(resultMessage, language.Id, _currentUser.UserId);

                return _mapper.Map<UserLanguageViewModel>(userlanguage);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error: {ex}", ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
