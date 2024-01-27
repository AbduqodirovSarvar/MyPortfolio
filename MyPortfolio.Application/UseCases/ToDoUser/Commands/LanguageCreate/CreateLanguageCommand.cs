using MediatR;
using MyPortfolio.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Entity.Enums;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.LanguageCreate
{
    public sealed class CreateLanguageCommand : IRequest<UserLanguage>
    {
        public CreateLanguageCommand(string name, long userId, LanguageLevel languageLevel)
        {
            Name = name;
            UserId = userId;
            LanguageLevel = languageLevel;
        }

        public string Name { get; set; }
        public long UserId { get; set; }
        public LanguageLevel LanguageLevel { get; set;}
    }
}
