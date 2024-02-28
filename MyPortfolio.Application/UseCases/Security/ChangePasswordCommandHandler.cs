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
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IAppDbContext _context;
        private readonly IHashService _hashService;

        public ChangePasswordCommandHandler(IAppDbContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
        }
        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken)
                                          ?? throw new NotFoundException();

            if(request.NewPassword == request.ConfirmPassword && _hashService.VerifyHash(request.OldPassword, user.Password))
            {
                user.ChangePassword(_hashService.GetHash(request.NewPassword));
                return (await _context.SaveChangesAsync(cancellationToken)) > 0;
            }
            return false;
        }
    }
}
