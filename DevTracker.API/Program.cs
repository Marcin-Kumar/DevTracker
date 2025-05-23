using DevTracker.API.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Context;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.ConfigureLogger();
        string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        if(string.IsNullOrWhiteSpace(connectionString))
        {
            Log.Fatal("Connection string 'DefaultConnection' is missing or empty. Shutting down");
            return;
        }

        builder.Services.ConfigureDependencyInjection(connectionString);
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        Log.Information("Building DevTracker API and configuring middleware...");
        var app = builder.Build();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(option => option.Theme = ScalarTheme.BluePlanet);
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        Log.Information("Starting DevTracker API...");
        app.Run();
    }
}