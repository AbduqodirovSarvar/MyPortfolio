using MediatR;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Behaviour;

namespace MyPortfolio.Application.UseCases.ToDoUser.Queries
{
    public class GetUserQuery : IRequest<UserViewModel>
    {
        public GetUserQuery(long id)
        {
            Id = id;
        }
        public GetUserQuery(string email)
        {
            Email = email;
        }

        public GetUserQuery(long id, string email)
        {
            Id = id;
            Email = email;
        }

        public long? Id { get; private set; } = null;
        [MailValidation]
        public string? Email { get; private set; } = null;
    }
}
