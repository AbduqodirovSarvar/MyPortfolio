using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.ExperienceDelete
{
    public sealed class DeleteExperienceCommand : IRequest<bool>
    {
        public DeleteExperienceCommand(long id) 
        {
            Id = id;
        }
        public long Id { get; set; }
    }
}
