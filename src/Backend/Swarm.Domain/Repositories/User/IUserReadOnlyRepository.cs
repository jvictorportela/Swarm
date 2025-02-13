namespace Swarm.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    Task<bool> ExistActiveUserWithEmail(string email);
    Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier);
    Task<Entities.User?> GetByEmailAndPassword(string email, string password);
}
