namespace Swarm.Domain.Repositories.Group;

public interface IGroupUpdateRepository
{
    Task<Entities.Group> GetGroupById(long id);
    void Update(Entities.Group group);
}
