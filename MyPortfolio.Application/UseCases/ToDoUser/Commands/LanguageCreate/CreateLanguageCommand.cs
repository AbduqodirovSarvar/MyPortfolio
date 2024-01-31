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
        public CreateLanguageCommand(string name, LanguageLevel languageLevel)
        {
            Name = name;
            LanguageLevel = languageLevel;
        }

        public string Name { get; set; }
        public LanguageLevel LanguageLevel { get; set;}
    }
}
