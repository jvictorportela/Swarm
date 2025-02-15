namespace Swarm.Domain.Repositories.Group;

public interface IGroupWriteOnlyRepository
{
    Task Add(Entities.Group group);
}
