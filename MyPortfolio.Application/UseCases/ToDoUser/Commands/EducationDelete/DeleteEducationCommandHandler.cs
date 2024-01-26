using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.EducationDelete
{
    public class DeleteEducationCommandHandler : IRequestHandler<DeleteEducationCommand, bool>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<DeleteEducationCommandHandler> _logger;
        public DeleteEducationCommandHandler(
            IAppDbContext context,
            ICurrentUserService currentUserService,
            ILogger<DeleteEducationCommandHandler> logger)
        {
            _logger = logger;
            _context = context;
            _currentUser = currentUserService;
        }
        public async Task<bool> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
        {
            var education = await _context.Educations
                                        .Where(x => x.UserId == _currentUser.UserId && x.Id == request.Id)
                                            .FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException("Certificate not found!");

            _context.Educations.Remove(education);

            bool result = (await _context.SaveChangesAsync(cancellationToken)) > 0;

            if (result)
            {
                _logger.LogInformation("Certificate (ID: {CertificateId}) removed by user (ID: {UserId})", education.Id, _currentUser.UserId);
            }

            return result;
        }
    }
}
