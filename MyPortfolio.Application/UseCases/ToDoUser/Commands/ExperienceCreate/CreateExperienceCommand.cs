using MediatR;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.ExperienceCreate
{
    public class CreateExperienceCommand : IRequest<ExperienceViewModel>
    {
        public CreateExperienceCommand(
            string companyName,
            string description,
            string position,
            WorkType workType,
            string city,
            DateOnly fromDate,
            DateOnly toDate
            )
        {
            CompanyName = companyName;
            Description = description;
            Position = position;
            WorkType = workType;
            City = city;
            FromDate = fromDate;
            ToDate = toDate;
        }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string Position { get; set; }
        public WorkType WorkType { get; set; }
        public string City { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
    }
}
