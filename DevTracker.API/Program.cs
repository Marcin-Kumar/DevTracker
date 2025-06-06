namespace DevTracker.API;

using DevTracker.API.Setup;
using DevTracker.API.Setup.Middleware;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.ConfigureLogger();
        

        builder.Services.ConfigureDependencyInjection(builder.Configuration);
        Log.Information("Building DevTracker API and configuring middleware...");
        var app = builder.Build();
        app.ConfigureMiddlewarePipeline();
        Log.Information("Starting DevTracker API...");
        app.Run();
    }
}