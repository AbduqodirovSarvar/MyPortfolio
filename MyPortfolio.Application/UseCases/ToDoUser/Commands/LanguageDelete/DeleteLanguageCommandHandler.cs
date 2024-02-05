using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.UseCases.ToDoUser.Commands.ExperienceDelete;
using MyPortfolio.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.LanguageDelete
{
    public sealed class DeleteLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand, bool>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<DeleteExperienceCommandHandler> _logger;
        private readonly IMapper _mapper;
        public DeleteLanguageCommandHandler(
            IAppDbContext context,
            ICurrentUserService currentUserService,
            ILogger<DeleteExperienceCommandHandler> logger,
            IMapper mapper
            )
        {
            _context = context;
            _currentUser = currentUserService;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = await _context.UserLanguages.Include(x => x.User)
                                         .FirstOrDefaultAsync(x => x.LanguageId == request.Id && x.UserId == _currentUser.UserId, cancellationToken)
                                         ?? throw new NotFoundException("Language not found!");

            _context.UserLanguages.Remove(language);

            bool result = (await _context.SaveChangesAsync(cancellationToken)) > 0;

            string resultMessage = result ? "Language (ID: {LanguageId}) removed by user (ID: {UserId})"
                                       : "Language (ID: {LanguageId}) couldn't remove by user (ID: {UserId})";

            _logger.LogInformation(resultMessage, language.LanguageId, _currentUser.UserId);

            return result;
        }
    }
}
