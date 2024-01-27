using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.UserDelete
{
    public sealed class DeleteUserCommand : IRequest<bool>
    {
    }
}
