using Microsoft.EntityFrameworkCore;
using Swarm.Domain.Entities;
using Swarm.Domain.Repositories.Group;

namespace Swarm.Infrastructure.DataAccess.Repositories;

public class GroupRepository : IGroupReadOnlyRepository, IGroupUpdateRepository, IGroupWriteOnlyRepository
{
    private readonly SwarmDbContext _context;

    public GroupRepository(SwarmDbContext context)
    {
        _context = context;
    }

    public async Task Add(Group group)
    {
        await _context.Groups.AddAsync(group);
    }

    public async Task<bool> ExistActiveGroupWithName(string name)
    {
        return await _context.Groups.AnyAsync(group => group.Name.Equals(name));
    }

    public async Task<List<Group>> GetAllGroups()
    {
        return await _context.Groups.ToListAsync();
    }

    public async Task<Group> GetGroupById(long id)
    {
        return await _context.Groups.FirstAsync(group => group.Id == id);
    }

    public void Update(Group group)
    {
        _context.Groups.Update(group);
    }
}
