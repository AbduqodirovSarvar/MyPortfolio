using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Models.ViewModels;

namespace MyPortfolio.Application.UseCases.ToDoUser.Queries
{
    public class GetUsersByFilterQueryHandler : IRequestHandler<GetUsersByFilterQuery, List<UserViewModel>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public GetUsersByFilterQueryHandler(
            IAppDbContext appDbContext,
            IMapper mapper)
        {
            _context = appDbContext;
            _mapper = mapper;
        }
        async Task<List<UserViewModel>> IRequestHandler<GetUsersByFilterQuery, List<UserViewModel>>.Handle(GetUsersByFilterQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users
                                .Include(x => x.Languages)
                                .ThenInclude(x => x.Language)
                                .Include(x => x.Projects)
                                .ThenInclude(x => x.Skills)
                                .ThenInclude(x => x.Skill)
                                .Include(x => x.Certificates)
                                .ThenInclude(x => x.Skills)
                                .ThenInclude(x => x.Skill)
                                .Include(x => x.Educations)
                                .Include(x => x.Experiences)
                                .ThenInclude(x => x.Skills)
                                .ThenInclude(x => x.Skill)
                                .Include(x => x.Skills)
                                .ThenInclude(x => x.Skill)
                                .Include(x => x.Socials)
                                .ToListAsync(cancellationToken);

            return _mapper.Map<List<UserViewModel>>(users);
        }
    }
}
