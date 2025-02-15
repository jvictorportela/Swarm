namespace Swarm.Domain.Entities;

public class Group : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public IList<Product> Products { get; set; } = []; // = new List<Product>();
    public long UserId { get; set; }
}