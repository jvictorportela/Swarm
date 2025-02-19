using Microsoft.EntityFrameworkCore;
using Swarm.Domain.Repositories.Product;

namespace Swarm.Infrastructure.DataAccess.Repositories;

public class ProductRepository : IProductReadOnlyRepository
{
    private readonly SwarmDbContext _context;

    public ProductRepository(SwarmDbContext context)
    {
        _context = context;
    }

    public async Task<long?> GetLastInternalCode()
    {
        return await _context.Products
            .OrderByDescending(p => p.InternalCode)
            .Select(p => (long?)p.InternalCode)
            .FirstOrDefaultAsync();
    }
}
