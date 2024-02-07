using MediatR;
using MyPortfolio.Application.Models.ViewModels;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.SocialCreate
{
    public sealed class CreateSocialNetworkCommand : IRequest<SocialViewModel>
    {
        public string SocialNetwork { get; set; } = null!;
        public string Url { get; set; } = null!;
    }
}
