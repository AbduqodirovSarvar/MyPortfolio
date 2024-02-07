using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Entities;
using MyPortfolio.Entity.Enums;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.SocialCreate
{
    public sealed class CreateSocialNetworkCommandHandler : IRequestHandler<CreateSocialNetworkCommand, SocialViewModel>
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
        async Task<SocialViewModel> IRequestHandler<CreateSocialNetworkCommand, SocialViewModel>.Handle(CreateSocialNetworkCommand request, CancellationToken cancellationToken)
        {
            var social = await _context.Socials
                                       .FirstOrDefaultAsync(x => x.SocialNetwork == (SocialNetwork)Enum.Parse(typeof(SocialNetwork), request.SocialNetwork)
                                                                    && x.UserId == _currentUser.UserId, cancellationToken)
                                       ?? new Social((SocialNetwork)Enum.Parse(typeof(SocialNetwork), request.SocialNetwork), request.Url, _currentUser.UserId);

            await _context.Socials.AddAsync(social, cancellationToken);

            string resultMessage = (await _context.SaveChangesAsync(cancellationToken)) > 0
                                          ? "Social network (ID: {socialId}) created by user (ID: {UserId})"
                                          : "Social network (ID: {socialID}) couldn't create by user (ID: {UserId})";

            _logger.LogInformation(resultMessage, social.Id, _currentUser.UserId);

            return _mapper.Map<SocialViewModel>(social);
        }
    }
}
