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
                option.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
                option
                .AddPreferredSecuritySchemes("BearerAuth")
                .AddHttpAuthentication("BearerAuth", auth => 
                    auth.Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdHVzZXIiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJVc2VyIiwiZXhwIjoxNzQ5MzEyMTk0LCJpc3MiOiJEZXZUcmFja2VyU2VydmVyIiwiYXVkIjoiRGV2VHJhY2tlclVzZXJzIn0.f9RIbUSb8gXzgK4Ap6yvKJwqRsNxqcvgJWUSBL2-Fuo");
            });
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}
