using MediatR;
using MyPortfolio.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.UserCreate
{
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserViewModel>
    {
        Task<UserViewModel> IRequestHandler<CreateUserCommand, UserViewModel>.Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
