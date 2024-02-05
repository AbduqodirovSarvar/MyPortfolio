using MediatR;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.LanguageUpdate
{
    public sealed class UpdateLanguageCommand : IRequest<UserLanguageViewModel>
    {
        [Required]
        public long LanguageId { get; set; }
        public string? LanguageLevel { get; set; } = null;
    }
}
