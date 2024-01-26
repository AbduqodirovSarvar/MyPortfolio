using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Application
{
    public static class DepencyInjection
    {
        public static IServiceCollection AddApplicationDepencyInjections(this IServiceCollection services)
        {
            return services;
        }
    }
}
