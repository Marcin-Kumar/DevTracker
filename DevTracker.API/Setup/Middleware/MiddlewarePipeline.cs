using Scalar.AspNetCore;

namespace DevTracker.API.Setup.Middleware;

public static class MiddlewarePipeline
{
    public static void ConfigureMiddlewarePipeline(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(options => options.RouteTemplate = "/openapi/{documentName}.json");
            app.MapScalarApiReference(option =>
            {
                option.Theme = ScalarTheme.BluePlanet;
            });
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}
