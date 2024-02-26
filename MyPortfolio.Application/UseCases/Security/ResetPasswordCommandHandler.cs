using MediatR;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Entity.Entities;
using MyPortfolio.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.Security
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IEmailService _emailService;
        private readonly IAppDbContext _context;
        private readonly IHashService _hashService;

        public ResetPasswordCommandHandler(IEmailService emailService, IAppDbContext context, IHashService hashService)
        {
            _emailService = emailService;
            _context = context;
            _hashService = hashService;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken) ?? throw new NotFoundException();

            if (_emailService.CheckEmailConfirmed(user.Email, request.ConfirmationCode.ToString()) && request.NewPassword == request.ConfirmNewPassword)
            {
                user.ChangePassword(_hashService.GetHash(request.NewPassword));
                return (await _context.SaveChangesAsync(cancellationToken)) > 0;
            }
            return false;
        }
    }
}
