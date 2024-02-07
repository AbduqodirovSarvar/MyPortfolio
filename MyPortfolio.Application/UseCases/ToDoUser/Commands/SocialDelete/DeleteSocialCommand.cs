using MediatR;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.SocialDelete
{
    public sealed class DeleteSocialCommand : IRequest<bool>
    {
        public DeleteSocialCommand(long socialId)
        {
            Id = socialId;
        }
        public long Id { get; set; }
    }
}
