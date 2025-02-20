using Agenda.Infrastructure.Database.Context.Configurations;
using Agenda.Infrastructure.Database.Entities.Agenda;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infrastructure.Database.Context
{
    public class AgendaDbContext : DbContext
    {
        public AgendaDbContext(DbContextOptions<AgendaDbContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<AgendaEntity> Agendas { get; set; }
        public DbSet<EventEntity> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AgendaConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}