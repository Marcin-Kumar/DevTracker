using DevTracker.API.Mappers;
using DevTracker.Core.Application.Adapters;
using DevTracker.Core.Application.InboundPorts;
using DevTracker.Data.Adapters;
using DevTracker.Data.Context;
using DevTracker.Data.Mappers;
using DevTracker.Domain.Ports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;

namespace DevTracker.API.Setup;

public static class DependencyInjection
{
    public static void ConfigureDependencyInjection(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DevTrackerContext>(options =>
                    options.UseSqlServer(connectionString,
                    sqloptions => sqloptions.MigrationsAssembly("DevTracker.Data"))
                );


        services.AddTransient<GoalDataMapper>();
        services.AddTransient<ProjectDataMapper>();
        services.AddTransient<SessionDataMapper>();

        services.AddTransient<GoalDtoMapper>();
        services.AddTransient<ProjectDtoMapper>();
        services.AddTransient<SessionDtoMapper>();

        services.AddScoped<IGoalRepository, GoalRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();

        services.AddScoped<IGoalService, GoalService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ISessionService, SessionService>();

        services.AddControllers();
        services.AddOpenApi();
        services.AddApiVersioning(option =>
        {
            option.AssumeDefaultVersionWhenUnspecified = true;
            option.DefaultApiVersion = new ApiVersion(1, 0);
            option.ReportApiVersions = true;
            option.ApiVersionReader = new UrlSegmentApiVersionReader();
        });
    }
}
