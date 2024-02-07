using MediatR;
using MyPortfolio.Application.Models.ViewModels;
using MyPortfolio.Entity.Behaviour;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Application.UseCases.Security
{
    public class AuthCommand : IRequest<LoginViewModel>
    {
        public AuthCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
        [Required]
        [MailValidation]
        public string Email { get; set; }
        [Required]
        [PasswordValidation]
        public string Password { get; set; }
    }
}
