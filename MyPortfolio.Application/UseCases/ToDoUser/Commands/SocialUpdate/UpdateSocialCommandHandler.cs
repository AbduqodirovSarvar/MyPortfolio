using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Entities;
using MyPortfolio.Entity.Enums;
using MyPortfolio.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.SocialUpdate
{
    public sealed class UpdateSocialCommandHandler : IRequestHandler<UpdateSocialCommand, SocialViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateSocialCommandHandler> _logger;
        public UpdateSocialCommandHandler(IAppDbContext context, ICurrentUserService currentUser, IMapper mapper, ILogger<UpdateSocialCommandHandler> logger)
        {
            _context = context;
            _currentUser = currentUser;
            _mapper = mapper;
            _logger = logger;
        }

        async Task<SocialViewModel> IRequestHandler<UpdateSocialCommand, SocialViewModel>.Handle(UpdateSocialCommand request, CancellationToken cancellationToken)
        {
            var social = await _context.Socials
                                       .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == _currentUser.UserId, cancellationToken)
                                       ?? throw new NotFoundException("Social network not found for the user");

            var changedSocial = new Social(
                                        (SocialNetwork)Enum.Parse(typeof(SocialNetwork), request.SocialNetwork),
                                        request.Url
                                        );

            social.Change(changedSocial);

            bool result = (await _context.SaveChangesAsync(cancellationToken)) > 0;

            string resultMessage = result ? "Social network (ID: {socialId}) updated by user (ID: {UserId})"
                                      : "Social network (ID: {socialID}) couldn't update by user (ID: {UserId})";

            _logger.LogInformation(resultMessage, social.Id, _currentUser.UserId);

            return _mapper.Map<SocialViewModel>(social);
        }
    }
}
