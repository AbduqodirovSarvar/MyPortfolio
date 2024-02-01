using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Exceptions;
using MyPortfolio.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.Security
{
    public class AuthCommandHandler : IRequestHandler<AuthCommand, LoginViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IHashService _hashService;
        private readonly ILogger<AuthCommandHandler> _logger;
        private readonly IMapper _mapper;
        public AuthCommandHandler(
            IAppDbContext context,
            ITokenService tokenService, 
            IHashService hashService,
            ILogger<AuthCommandHandler> logger,
            IMapper mapper)
        {
            _context = context;
            _tokenService = tokenService;
            _hashService = hashService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<LoginViewModel> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                                     .FirstOrDefaultAsync(x => x.Email == request.Email
                                                          && _hashService.VerifyHash(request.Password, x.Password), cancellationToken)
                                     ?? throw new LoginException();

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                };

            _logger.LogInformation("Gave access token for identifier Id: {Identifier}", user.Id);

            return new LoginViewModel(_mapper.Map<UserViewModel>(user), _tokenService.GetAccessToken(claims.ToArray()));
        }
    }
}
