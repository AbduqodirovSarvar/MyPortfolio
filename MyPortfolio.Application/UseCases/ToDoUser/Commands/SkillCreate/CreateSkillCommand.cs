using MediatR;
using Microsoft.AspNetCore.Http;
using MyPortfolio.Application.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.SkillCreate
{
    public sealed class CreateSkillCommand : IRequest<SkillViewModel>
    {
        [Required]
        public string Name { get; set; } = default!;
        public IFormFile? Photo { get; set; }

    }
}
