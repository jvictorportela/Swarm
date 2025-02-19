namespace Swarm.Domain.Repositories.Product;

public interface IProductReadOnlyRepository
{
    Task<long?> GetLastInternalCode();
}
