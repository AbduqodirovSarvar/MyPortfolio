using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<GetUserQueryHandler> _logger;
        private readonly IMapper _mapper;
        public GetUserQueryHandler(
            IAppDbContext appDbContext,
            ILogger<GetUserQueryHandler> logger,
            IMapper mapper)
        {
            _context = appDbContext;
            _logger = logger;
            _mapper = mapper;
        }
        async Task<UserViewModel> IRequestHandler<GetUserQuery, UserViewModel>.Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                                .Include(x => x.Languages)
                                .Include(x => x.Projects)
                                .Include(x => x.Certificates)
                                .Include(x => x.Educations)
                                .Include(x => x.Experiences)
                                .Include(x => x.Skills)
                                .Include(x => x.Socials)
                                .FirstOrDefaultAsync(x => (x.Id == request.Id) || (x.Email == request.Email), cancellationToken);

            if(user == null)
            {
                _logger.LogInformation("Attempting to find user with {identifierType} identifier: {identifierValue}",
                     request.Id != null ? "Id" : "Email",
                     request.Id != null ? request.Id : request.Email);

                throw new NotFoundException("User was not found");
            }

            return _mapper.Map<UserViewModel>(user);
        }
    }
}
