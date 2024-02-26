using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Abstractions.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string email, string subject, string body);
        Task<bool> SendEmailConfirmed(string email);
        bool CheckEmailConfirmed(string email, string confirmationCode);
    }
}
