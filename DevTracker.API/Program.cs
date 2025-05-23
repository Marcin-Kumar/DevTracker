using DevTracker.API.Setup;
using DevTracker.API.Setup.Middleware;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
        Log.Information("Building DevTracker API and configuring middleware...");
        var app = builder.Build();
        app.ConfigureMiddlewarePipeline();
        Log.Information("Starting DevTracker API...");
        app.Run();
    }
}