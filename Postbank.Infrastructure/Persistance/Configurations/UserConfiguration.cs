using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postbank.Domain.Models;
using Postbank.Domain.Models.ValueObjects;

namespace Postbank.Infrastructure.Persistance.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email)
                .HasConversion(to => to.Value, from => Email.Create(from));

            builder.HasMany(x => x.TimeLogs)
                .WithOne(x=>x.User)
                .HasForeignKey(x=>x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
