using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Mappings;
using MyPortfolio.Application.Services;
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
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DepencyInjection).Assembly);
            });

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IFileService, FileService>();
            var mappingconfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingconfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
