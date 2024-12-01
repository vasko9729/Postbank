using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postbank.Domain.Models;
using Postbank.Domain.Models.ValueObjects;

namespace Postbank.Infrastructure.Persistance.Configurations;

internal class TimeLogConfiguration : IEntityTypeConfiguration<TimeLog>
{
    public void Configure(EntityTypeBuilder<TimeLog> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Time)
            .HasConversion(to => to.Value, from => Time.Create(from));

        builder.HasOne(x => x.User)
            .WithMany(x => x.TimeLogs)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Project)
            .WithMany(x => x.TimeLogs)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

