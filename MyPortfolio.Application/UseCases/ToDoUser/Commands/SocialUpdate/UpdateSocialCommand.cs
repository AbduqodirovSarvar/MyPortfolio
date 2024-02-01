using MediatR;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Behaviour;
using MyPortfolio.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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