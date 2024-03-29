﻿using MediatR;
using Microsoft.AspNetCore.Http;
using MyPortfolio.Application.Models.ViewModels;

namespace MyPortfolio.Application.UseCases.ToDoUser.Commands.UserCreate
{
    public sealed class CreateUserCommand : IRequest<UserViewModel>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? MiddleName { get; set; }
        public DateOnly BirthDay { get; set; }
        public string Gender { get; set; } = null!;
        public string Profession { get; set; } = null!;
        public string AboutMe { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public IFormFile? Photo { get; set; }
        public IFormFile Resume { get; set; } = null!;

    }
}
