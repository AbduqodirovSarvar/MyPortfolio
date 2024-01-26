using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.EducationDelete
{
    public class DeleteEducationCommand : IRequest<bool>
    {
        public DeleteEducationCommand(long id)
        {
            Id = id;
        }
        public long Id { get; set; }
    }
}
