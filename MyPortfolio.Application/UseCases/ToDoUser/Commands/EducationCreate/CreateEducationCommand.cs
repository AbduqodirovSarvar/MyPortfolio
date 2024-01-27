using MediatR;
using MyPortfolio.Entity.Behaviour;
using MyPortfolio.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.EducationCreate
{
    public sealed class CreateEducationCommand : IRequest<Education>
    {
        public CreateEducationCommand(
            string name,
            string description,
            string city,
            DateOnly fromDate,
            DateOnly toDate,
            string url)
        {
            Name = name; 
            Description = description;
            City = city;
            FromDate = fromDate;
            ToDate = toDate;
            EducationWebSiteUrl = url;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public string EducationWebSiteUrl { get; set; }
    }
}
