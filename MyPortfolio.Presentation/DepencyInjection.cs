using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPortfolio.Application;
using MyPortfolio.Application.Services;
using MyPortfolio.Infrastructure;
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
        public static IServiceCollection PresentationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ApplicationServices();
            services.InfrastructureServices(configuration);
            services.AddControllers().AddApplicationPart(typeof(ApiController).Assembly);
            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                        //options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                    });

            return services;
        }
    }
}
