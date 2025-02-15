using Microsoft.EntityFrameworkCore;

namespace Swarm.Infrastructure.DataAccess;

public class SwarmDbContext : DbContext
{
    public SwarmDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Domain.Entities.User> Users { get; set; }
    public DbSet<Domain.Entities.Group> Groups { get; set; }
    public DbSet<Domain.Entities.Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SwarmDbContext).Assembly);
    }
}
