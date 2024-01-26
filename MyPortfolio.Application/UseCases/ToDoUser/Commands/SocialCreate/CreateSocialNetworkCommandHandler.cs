using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.SocialCreate
{
    public class CreateSocialNetworkCommandHandler : IRequestHandler<CreateSocialNetworkCommand, SocialViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<CreateSocialNetworkCommandHandler> _logger;
        private readonly IMapper _mapper;
        public CreateSocialNetworkCommandHandler(
            IAppDbContext appDbContext,
            ICurrentUserService currentUserService,
            ILogger<CreateSocialNetworkCommandHandler> logger,
            IMapper mapper
            )
        {
            _context = appDbContext;
            _currentUser = currentUserService;
            _mapper = mapper;
            _logger = logger;
        }
        Task<SocialViewModel> IRequestHandler<CreateSocialNetworkCommand, SocialViewModel>.Handle(CreateSocialNetworkCommand request, CancellationToken cancellationToken)
        {

            throw new NotImplementedException();
        }
    }
}
