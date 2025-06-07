using DevTracker.API.Mappers;
using DevTracker.Core.Application.Adapters;
using DevTracker.Core.Application.InboundPorts;
using DevTracker.Data.Adapters;
using DevTracker.Data.Context;
using DevTracker.Data.Mappers;
using DevTracker.Domain.Ports;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace DevTracker.API.Setup;

public static class DependencyInjection
{
    public static void ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            Log.Fatal("Connection string 'DefaultConnection' is missing or empty. Shutting down");
            return;
        }

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

        services.AddScoped<ILoginService, LoginService>();

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false));
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            options.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme { 
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "JWT Authorization header using the Bearer scheme.",
                In = ParameterLocation.Header,
            });
            //TODO: Remove global security requirement and add a custom operation filter to omit LoginController
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                         Reference = new OpenApiReference
                         {
                            Id = "BearerAuth",
                            Type = ReferenceType.SecurityScheme
                         }
                    },
                    Array.Empty<string>()
                }
            });
        });

        services.AddApiVersioning(option =>
        {
            option.AssumeDefaultVersionWhenUnspecified = true;
            option.DefaultApiVersion = new ApiVersion(1, 0);
            option.ReportApiVersions = true;
            option.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        IConfigurationSection jwtSettings = configuration.GetSection("JwtSettings");
        string? secretKey = jwtSettings["Key"];
        if (string.IsNullOrWhiteSpace(secretKey))
        {
            Log.Fatal("Json web Token Secret Key is missing or empty. Shutting down");
            return;
        }
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ClockSkew = TimeSpan.Zero
            };
        });
        services.AddAuthorization();
    }
}