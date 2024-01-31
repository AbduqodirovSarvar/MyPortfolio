using MediatR;
using MyPortfolio.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.LanguageDelete
{
    public sealed class DeleteLanguageCommand : IRequest<bool>
    {
        public DeleteLanguageCommand(long id)
        {
            Id = id;
        }
        public long Id { get; set; }
    }
}
