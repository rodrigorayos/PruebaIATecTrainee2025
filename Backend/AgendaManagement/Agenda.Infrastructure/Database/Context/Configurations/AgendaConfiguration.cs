using Agenda.Infrastructure.Database.Entities.Agenda;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Infrastructure.Database.Context.Configurations;

public class AgendaConfiguration : IEntityTypeConfiguration<AgendaEntity>
{
    public void Configure(EntityTypeBuilder<AgendaEntity> builder)
    {
        builder.ToTable("Agenda", "AGD");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasColumnName("id")
            .UseIdentityColumn();

        builder.Property(a => a.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.Description)
            .HasColumnName("description")
            .HasMaxLength(255);

        builder.Property(a => a.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(a => a.CreatedBy)
            .HasColumnName("created_by")
            .IsRequired();

        builder.Property(a => a.LastModifiedByAt)
            .HasColumnName("last_modified_by_at")
            .IsRequired();

        builder.Property(a => a.LastModifiedBy)
            .HasColumnName("last_modified_by")
            .IsRequired();

        builder.HasOne(a => a.User)
            .WithMany(u => u.Agendas)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}