using MediatR;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.Security
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, bool>
    {
        private readonly IEmailService _emailService;
        private readonly IAppDbContext _context;

        public ForgotPasswordCommandHandler(IAppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken)
                                           ?? throw new NotFoundException();

            return await _emailService.SendEmailConfirmed(request.Email);
        }
    }
}
