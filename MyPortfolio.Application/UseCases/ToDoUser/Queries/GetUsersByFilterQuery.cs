using MediatR;
using MyPortfolio.Application.Models.ViewModels;

namespace MyPortfolio.Application.UseCases.ToDoUser.Queries
{
    public class GetUsersByFilterQuery : IRequest<List<UserViewModel>>
    {
        public GetUsersByFilterQuery() { }
    }
}
