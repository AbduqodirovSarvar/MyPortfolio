using MediatR;
using MyPortfolio.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands
{
    public class CreateUserCommand : IRequest<UserViewModel>
    {

    }
}
