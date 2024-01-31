using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.SkillDelete
{
    public sealed class DeleteSkillCommand : IRequest<bool>
    {
        public DeleteSkillCommand(long skillId)
        {
            Id = skillId;
        }
        public long Id { get; set; }
    }
}
