using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyPortfolio.Infrastructure.DbContexts;
using MyPortfolio.Presentation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();
builder.Services.PresentationServices(builder.Configuration);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "My Portfolio",
        Description = "My portfolio website's backend"
    });

    options.AddSecurityDefinition("Beareer", new OpenApiSecurityScheme()
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Authorization",
        Type = SecuritySchemeType.Http
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Id = "Beareer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }

    });

});

var app = builder.Build();
// Add CORS configuration
app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

try
{
    // Apply migrations on startup
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
    Console.WriteLine($"Migrations applying succesfully completed");
}
catch (Exception ex)
{
    Console.WriteLine($"Error applying migrations: {ex.Message}");
}

app.Run();
