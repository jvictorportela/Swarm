namespace Swarm.Communication.Requests;

public class RequestGroupJson
{
    public string Name { get; set; } = string.Empty;
    public IList<RequestProductJson> Products { get; set; } = []; // = new List<Product>();
    public long UserId { get; set; }
}
