using MediatR;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Behaviour;
using MyPortfolio.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.ProjectCreate
{
    public sealed class CreateProjectCommand : IRequest<Project>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IFormFile Photo { get; set; } = null!;
        public string UrlToCode { get; set; } = null!;
        public string UrlToSite { get; set; } = null!;
        public ICollection<string> Skills { get; set; } = new List<string>();
    }
}
