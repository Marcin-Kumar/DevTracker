using DevTracker.API.Mappers;
using DevTracker.Data.Adapters;
using DevTracker.Data.Context;
using DevTracker.Data.Mappers;
using DevTracker.Domain.Application;
using DevTracker.Domain.InboundPorts;
using DevTracker.Domain.Ports;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<DevTrackerContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
            sqloptions => sqloptions.MigrationsAssembly("DevTracker.Data"))
        );


        builder.Services.AddScoped<GoalDataMapper>();
        builder.Services.AddScoped<ProjectDataMapper>();
        builder.Services.AddScoped<SessionDataMapper>();

        builder.Services.AddScoped<GoalDtoMapper>();
        builder.Services.AddScoped<ProjectDtoMapper>();
        builder.Services.AddScoped<SessionDtoMapper>();

        builder.Services.AddScoped<IGoalRepository, GoalRepository>();
        builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
        builder.Services.AddScoped<ISessionRepository, SessionRepository>();

        builder.Services.AddScoped<IGoalService, GoalService>();
        builder.Services.AddScoped<IProjectService, ProjectService>();
        builder.Services.AddScoped<ISessionService, SessionService>();


        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(option => option.Theme = ScalarTheme.BluePlanet);
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}