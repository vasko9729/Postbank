using Microsoft.EntityFrameworkCore;
using Postbank.Domain.Models;

namespace Postbank.Infrastructure.Persistance;

internal class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {

    }
    public  DbSet<User> Users { get; set; }

    public DbSet<Project> Projects { get; set; }

    public DbSet<TimeLog> TimeLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

}
