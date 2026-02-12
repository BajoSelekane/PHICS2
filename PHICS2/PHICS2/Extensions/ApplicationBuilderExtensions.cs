using Microsoft.AspNetCore.Builder;

namespace Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerWithUi(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwagger();

        return app;
    }
}
