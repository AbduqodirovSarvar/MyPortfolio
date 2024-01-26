using MediatR;
using MyPortfolio.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Queries
{
    public class GetUsersByFilterQuery : IRequest<List<UserViewModel>>
    {
        public GetUsersByFilterQuery() { }
    }
}
