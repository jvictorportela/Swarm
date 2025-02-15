namespace Swarm.Domain.Repositories.Group;

public interface IGroupReadOnlyRepository
{
    Task<List<Entities.Group>> GetAllGroups();
    Task<bool> ExistActiveGroupWithName(string name);
}
