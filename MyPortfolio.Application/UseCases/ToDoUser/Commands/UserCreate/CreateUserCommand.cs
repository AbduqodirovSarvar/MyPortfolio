using MediatR;
using Microsoft.AspNetCore.Http;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Behaviour;
using MyPortfolio.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.UserCreate
{
    public sealed class CreateUserCommand : IRequest<UserViewModel>
    {
        public CreateUserCommand(
            string firstName,
            string lastName,
            string email,
            string password,
            string? middleName,
            DateOnly birthDay,
            string gender,
            string professional,
            string aboutMe,
            string phoneNumber,
            IFormFile? photo,
            IFormFile resume
            )
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            MiddleName = middleName;
            BirthDay = birthDay;
            Gender = gender;
            Profession = professional;
            AboutMe = aboutMe;
            PhoneNumber = phoneNumber;
            Photo = photo;
            Resume = resume;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? MiddleName { get; set; }
        public DateOnly BirthDay { get; set; }
        public string Gender { get; set; }
        public string Profession { get; set; }
        public string AboutMe { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile? Photo { get; set; }
        public IFormFile Resume { get; set; }

    }
}
