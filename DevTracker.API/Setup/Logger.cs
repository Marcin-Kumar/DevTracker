using Serilog;

namespace DevTracker.API.Setup;

public static class Logger
{
    public static void ConfigureLogger(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();
        builder.Host.UseSerilog();
    }
}
