using MediatR;
using MyPortfolio.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.SkillCreate
{
    public sealed class CreateSkillCommand : IRequest<List<UserSkill>>
    {
        public CreateSkillCommand(List<string> names)
        {
            Names = names;
        }
        public List<string> Names { get; set; }
    }
}
