using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.ProjectDelete
{
    public sealed class DeleteProjectCommand : IRequest<bool>
    {
        public DeleteProjectCommand(long id) 
        {
            Id = id;
        }
        public long Id { get; set; }
    }
}
