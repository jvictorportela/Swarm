using Microsoft.EntityFrameworkCore;
using Swarm.Domain.Entities;
using Swarm.Domain.Repositories.User;

namespace Swarm.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
{
    private readonly SwarmDbContext _context;

    public UserRepository(SwarmDbContext context)
    {
        _context = context;
    }

    public async Task Add(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email.Equals(email) && u.Active);
    }

    public async Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier)
    {
        return await _context.Users.AnyAsync(user => user.UserIdentifier.Equals(userIdentifier) && user.Active);
    }

    public async Task<User?> GetByEmailAndPassword(string email, string password)
    {
        return await _context
            .Users
            .AsNoTracking() //Quando não vai ser atualizada por regra de negócio
            .FirstOrDefaultAsync(user => user.Active && user.Email.Equals(email) && user.Password.Equals(password));
    }

    public async Task<User> GetById(long id)
    {
        return await _context
            .Users
            .FirstAsync(user => user.Id == id);
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }
}
