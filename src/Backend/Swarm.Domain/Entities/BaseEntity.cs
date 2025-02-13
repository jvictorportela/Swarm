namespace Swarm.Domain.Entities;

public class BaseEntity
{
    public long Id { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public bool Active { get; set; } = true;
}
