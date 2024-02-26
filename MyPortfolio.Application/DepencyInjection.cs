using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MyPortfolio.Application.Abstractions.Interfaces;
using MyPortfolio.Application.Mappings;
using MyPortfolio.Application.Services;

namespace MyPortfolio.Application
{
    public static class DepencyInjection
    {
        public static void ApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DepencyInjection).Assembly);
            });

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IEmailService, EmailService>();
            var mappingconfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingconfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
