using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application.Abstractions.Interfaces
{
    public interface ILogInformation
    {
        Task Logging(string name);
    }
}
