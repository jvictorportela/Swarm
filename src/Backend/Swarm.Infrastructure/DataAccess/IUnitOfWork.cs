using Swarm.Domain.Repositories;

namespace Swarm.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly SwarmDbContext _context;

    public UnitOfWork(SwarmDbContext context)
    {
        _context = context;
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}
