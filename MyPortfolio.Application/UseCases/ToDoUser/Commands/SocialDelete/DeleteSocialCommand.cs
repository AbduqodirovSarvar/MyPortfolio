using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
