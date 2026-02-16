using Microsoft.EntityFrameworkCore;
using PHICS2Web.Data;

namespace PHICS2Web.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using Infrastructure.DataLayer.ApplicationDbContext dbContext =
            scope.ServiceProvider.GetRequiredService<Infrastructure.DataLayer.ApplicationDbContext>();

        dbContext.Database.Migrate();
    }
}
