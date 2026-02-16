using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MediatR;

namespace PHICS2.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Use your real connection string here
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-84LNG8G\\SQLEXPRESS01;Database=PHCIS2DB;TrustServerCertificate=True;Trusted_Connection=True;");

            // For design-time, mediator can be null
            return new ApplicationDbContext(optionsBuilder.Options, mediator: null!);
        }
    }
}
