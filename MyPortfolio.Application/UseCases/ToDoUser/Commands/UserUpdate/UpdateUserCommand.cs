using MediatR;
using Microsoft.AspNetCore.Http;
using MyPortfolio.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.UserUpdate
{
    public sealed class UpdateUserCommand : IRequest<UserViewModel>
    {
        [Required]
        public long Id { get; set; }
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? Password { get; set; } = null;
        public string? MiddleName { get; set; } = null;
        public DateOnly? BirthDay { get; set; } = null;
        public string? Gender { get; set; } = null;
        public string? Profession { get; set; } = null;
        public string? AboutMe { get; set; } = null;
        public string? PhoneNumber { get; set; } = null;
        public IFormFile? Photo { get; set; } =  null;
        public IFormFile? Resume { get; set; } = null;
    }
}
