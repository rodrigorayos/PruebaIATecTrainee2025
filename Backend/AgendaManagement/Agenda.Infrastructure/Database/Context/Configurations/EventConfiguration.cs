using Agenda.Infrastructure.Database.Entities.Agenda;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agenda.Infrastructure.Database.Context.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<EventEntity>
{
    public void Configure(EntityTypeBuilder<EventEntity> builder)
    {
        builder.ToTable("Event", "AGD");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .UseIdentityColumn();

        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasColumnName("description")
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(e => e.Date)
            .HasColumnName("date")
            .IsRequired();

        builder.Property(e => e.Location)
            .HasColumnName("location")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(e => e.Participants)
            .HasColumnName("participants")
            .HasDefaultValue(string.Empty);

        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(e => e.CreatedBy)
            .HasColumnName("created_by")
            .IsRequired();

        builder.Property(e => e.LastModifiedByAt)
            .HasColumnName("last_modified_by_at")
            .IsRequired();

        builder.Property(e => e.LastModifiedBy)
            .HasColumnName("last_modified_by")
            .IsRequired();

        builder.HasOne(e => e.Agenda)
            .WithMany(a => a.Events)
            .HasForeignKey(e => e.AgendaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}