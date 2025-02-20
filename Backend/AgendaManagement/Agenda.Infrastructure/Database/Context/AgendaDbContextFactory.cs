using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Agenda.Infrastructure.Database.Context
{
    public class AgendaDbContextFactory : IDesignTimeDbContextFactory<AgendaDbContext>
    {
        public AgendaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AgendaDbContext>();
            optionsBuilder.UseSqlServer("Server=RAYO;Database=AgendaDb;User Id=sa;Password=1234;TrustServerCertificate=True;");

            return new AgendaDbContext(optionsBuilder.Options);
        }

        public static AgendaDbContext Create(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AgendaDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AgendaDbContext(optionsBuilder.Options);
        }
    }
}