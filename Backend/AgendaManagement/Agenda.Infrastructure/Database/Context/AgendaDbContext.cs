using Microsoft.EntityFrameworkCore;
using Agenda.Infrastructure.Database.Entities.Common;
using Agenda.Infrastructure.Database.Entities.Agenda;
using Agenda.Infrastructure.Database.Configurations.Agenda;

namespace Agenda.Infrastructure.Database;

public class AgendaDbContext : DbContext
{
    public DbSet<EventEntity> Events { get; set; }
    
    public AgendaDbContext(DbContextOptions<AgendaDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new EventConfiguration());
    }

    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditFields()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = GetCurrentUserId();
                    entry.Entity.LastModifiedByAt = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = GetCurrentUserId();
                    break;

                case EntityState.Modified:
                    entry.Property(nameof(BaseEntity.CreatedAt)).IsModified = false;
                    entry.Property(nameof(BaseEntity.CreatedBy)).IsModified = false;
                    entry.Entity.LastModifiedByAt = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = GetCurrentUserId();
                    break;
            }
        }
    }

    private int GetCurrentUserId()
    {
        return 1;
    }
}