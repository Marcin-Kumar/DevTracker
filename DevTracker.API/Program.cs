using DevTracker.Data.Context;
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