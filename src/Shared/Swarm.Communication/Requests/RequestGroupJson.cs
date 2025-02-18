namespace Swarm.Communication.Requests;

public class RequestGroupJson
{
    public string Name { get; set; } = string.Empty;
    public IList<RequestProductJson> Products { get; set; } = [];
    public long UserId { get; set; }
}
