using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.SkillDelete
{
    public sealed class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, bool>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly ILogger<DeleteSkillCommandHandler> _logger;
        public DeleteSkillCommandHandler(
            IAppDbContext context,
            ICurrentUserService currentUser,
            ILogger<DeleteSkillCommandHandler> logger)
        {
            _context = context;
            _currentUser = currentUser;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var (user, userSkill) = await _context.Users
                                    .Include(x => x.Skills)
                                    .Where(x => x.Id == _currentUser.UserId)
                                    .Select(x => Tuple.Create(x, x.Skills.FirstOrDefault(y => y.SkillId == request.Id)))
                                    .FirstOrDefaultAsync(cancellationToken)
                                    ?? throw new NotFoundException("User");

                if (user == null || userSkill == null)
                {
                    throw new NotFoundException("User or skill not found");
                }

                user.Skills.Remove(userSkill);

                bool result = (await _context.SaveChangesAsync(cancellationToken)) > 0;

                string resultMessage = result ? "Skill (ID: {SkillId}) removed by user (ID: {UserId})"
                                          : "Skill (ID: {SkillId}) couldn't remove by user (ID: {UserId})";

                _logger.LogInformation(resultMessage, userSkill?.SkillId, _currentUser.UserId);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
