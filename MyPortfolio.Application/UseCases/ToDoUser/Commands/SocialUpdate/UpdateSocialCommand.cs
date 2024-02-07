using MediatR;
using MyPortfolio.Application.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.SocialUpdate
{
    public sealed class UpdateSocialCommand : IRequest<SocialViewModel>
    {
        [Required]
        public long Id { get; set; }
        public string? SocialNetwork { get; set; }
        public string? Url { get; set; }
    }
}