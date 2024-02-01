using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Entity.Entities;
using MyPortfolio.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.SkillCreate
{
    public sealed class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, List<UserSkill>>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<CreateSkillCommandHandler> _logger;
        public CreateSkillCommandHandler(
            IAppDbContext context, 
            ICurrentUserService currentUser,
            ILogger<CreateSkillCommandHandler> logger)
        {
            _context = context;
            _currentUser = currentUser;
            _logger = logger;
        }

        public async Task<List<UserSkill>> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Include(x => x.Skills)
                                           .FirstOrDefaultAsync(x => x.Id == _currentUser.UserId, cancellationToken)
                                           ?? throw new NotFoundException("User not found");

            user.Skills.ToList().AddRange(
                            from name in request.Names
                            let skill = _context.Skills.FirstOrDefault(x => x.Name == name) ?? new Skill(name)
                            select new UserSkill(skill, user.Id));

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("User's skills updated by user identifier Id: {userId}", _currentUser.UserId);

            return user.Skills.ToList();
        }
    }
}
