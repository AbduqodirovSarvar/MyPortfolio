using Microsoft.Extensions.DependencyInjection;
using MyPortfolio.Application;
using MyPortfolio.Presentation.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Presentation
{
    public static class DepencyInjection
    {
        public static IServiceCollection PresentationServices(this IServiceCollection services)
        {
            services.AddApplicationDepencyInjections();
            return services;
        }
    }
}
