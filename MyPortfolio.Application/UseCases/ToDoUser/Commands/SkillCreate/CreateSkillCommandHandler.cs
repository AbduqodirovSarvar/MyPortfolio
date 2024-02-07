using AutoMapper;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Models.ViewModels;
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
    public sealed class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, List<SkillViewModel>>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<CreateSkillCommandHandler> _logger;
        private readonly IMapper _mapper;
        public CreateSkillCommandHandler(
            IAppDbContext context,
            ICurrentUserService currentUser,
            ILogger<CreateSkillCommandHandler> logger,
            IMapper mapper
            )
        {
            _context = context;
            _currentUser = currentUser;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<SkillViewModel>> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(x => x.Skills).ThenInclude(x => x.Skill)
                .FirstOrDefaultAsync(x => x.Id == _currentUser.UserId, cancellationToken)
                ?? throw new NotFoundException("User not found");

            var (existedSkills, newSkills) = request.Names
                                            .Select(skillName => (
                                                Skill: _context.Skills.FirstOrDefault(s => s.Name == skillName),
                                                SkillName: skillName,
                                                ExistingUserSkill: user.Skills.Select(x => x.Skill).FirstOrDefault(s => s?.Name == skillName)
                                            ))
                                            .Aggregate(
                                                (Existed: new List<Skill>(), New: new List<Skill>()),
                                                (result, tuple) =>
                                                {
                                                    if (tuple.Skill == null)
                                                    {
                                                        if (tuple.ExistingUserSkill == null)
                                                        {
                                                            result.New.Add(new Skill(tuple.SkillName));
                                                        }
                                                        else
                                                        {
                                                            result.Existed.Add(tuple.ExistingUserSkill);
                                                        }
                                                    }
                                                    return result;
                                                },
                                                result => (result.Existed, result.New)
                                            );

            await _context.Skills.AddRangeAsync(newSkills, cancellationToken);
            existedSkills.AddRange(newSkills);

            var userSkillCreateList = existedSkills
                .Select(skill => new UserSkill(skill, user))
                .ToList();

            await _context.UserSkills.AddRangeAsync(userSkillCreateList, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            string resultMessage = (await _context.SaveChangesAsync(cancellationToken)) > 0
                                        ? $"Skills added by user (ID: {_currentUser.UserId})"
                                        : $"Skills couldn't be added by user (ID: {_currentUser.UserId})";

            _logger.LogInformation(resultMessage, _currentUser.UserId);

            return _mapper.Map<List<SkillViewModel>>(user.Skills.Select(x => x.Skill).ToList());
        }

    }
}
