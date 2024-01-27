using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.LanguageCreate
{
    public sealed class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, UserLanguage>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<CreateLanguageCommandHandler> _logger;
        public CreateLanguageCommandHandler(
            IAppDbContext appDbContext,
            ICurrentUserService currentUserService,
            ILogger<CreateLanguageCommandHandler> logger
            )
        {
            _context = appDbContext;
            _currentUser = currentUserService;
            _logger = logger;
        }
        public async Task<UserLanguage> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var language = await _context.Languages.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
                if (language == null)
                {
                    language = new Language(request.Name);
                    await _context.Languages.AddAsync(language, cancellationToken);
                }

                var userlanguage = new UserLanguage(
                    language.Id,
                    _currentUser.UserId,
                    request.LanguageLevel
                    );
                return userlanguage;
            }
            catch ( Exception ex )
            {
                _logger.LogInformation("Error: {ex}", ex.Message);
                throw new Exception(ex.Message);
            }
            
        }
    }
}
