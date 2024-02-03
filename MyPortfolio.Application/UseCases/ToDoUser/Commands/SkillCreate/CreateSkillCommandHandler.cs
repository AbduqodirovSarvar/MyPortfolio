using MediatR;
using Microsoft.Data.Sqlite;
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

            var user = await _context.Users
                                .Include(x => x.Skills).ThenInclude(x => x.Skill)
                                .FirstOrDefaultAsync(x => x.Id == _currentUser.UserId, cancellationToken)
                                ?? throw new NotFoundException("User not found");

            var (existedSkills, newSkills) = (
                                            from skillName in request.Names
                                            join skill in _context.Skills on skillName equals skill.Name into matchingSkills
                                            from matchedSkill in matchingSkills.DefaultIfEmpty()
                                            select (matchedSkill, skillName)
                                        )
                                        .Aggregate(
                                            (existed: new List<Skill>(), newSkills: new List<string>()),
                                            (acc, tuple) =>
                                            {
                                                if (tuple.matchedSkill != null)
                                                {
                                                    acc.existed.Add(tuple.matchedSkill);
                                                }
                                                else
                                                {
                                                    acc.newSkills.Add(tuple.skillName);
                                                }
                                                return acc;
                                            },
                                            result => (result.existed, result.newSkills.Select(name => new Skill(name)).ToList())
                                        );


            await _context.Skills.AddRangeAsync(newSkills, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            existedSkills.AddRange(newSkills);
            var userSkillList = existedSkills
                .Select(skill => new UserSkill(skill, user))
                .ToList();

            await _context.UserSkills.AddRangeAsync(userSkillList, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            /*var user = await _context.Users.Include(x => x.Skills).ThenInclude(x => x.Skill)
                                           .FirstOrDefaultAsync(x => x.Id == _currentUser.UserId, cancellationToken)
                                           ?? throw new NotFoundException("User not found");


            *//*var lists = (from name in request.Names
                         let skill = _context.Skills.FirstOrDefault(x => x.Name == name) ?? _context.Skills.Add(new Skill(name)).Entity
                         select new UserSkill(skill.Id, user.Id)).ToList();*/

            /*await _context.UserSkills.AddRangeAsync(from name in request.Names
                                                    let skill = _context.Skills.FirstOrDefault(x => x.Name == name) ?? _context.Skills.Add(new Skill(name)).Entity
                                                    select new UserSkill(skill, user), cancellationToken);*//*

            var skills = from name in request.Names
                         let skill = _context.Skills.FirstOrDefault(x => x.Name == name)
                         ?? _context.Skills.Add(new Skill(name)).Entity
                         select skill;

            await _context.SaveChangesAsync(cancellationToken);

            var userSkillList = from skill in _context.Skills where skills.Any(x => x.Name == skill.Name) select new UserSkill(skill, user);

            await _context.UserSkills.AddRangeAsync(userSkillList, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);*/
            /*
                        foreach(var userSkill in userSkillList)
                        {
                            await _context.UserSkills.AddAsync(userSkill, cancellationToken);
                            await _context.SaveChangesAsync(cancellationToken);
                        }*/

            _logger.LogInformation("User's skills updated by user identifier Id: {userId}", _currentUser.UserId);

            return user.Skills.ToList();
        }
    }
}
