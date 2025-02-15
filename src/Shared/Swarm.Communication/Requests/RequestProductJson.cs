using Swarm.Domain.Enums;

namespace Swarm.Communication.Requests;

public class RequestProductJson
{
    public long InternalCode { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Value { get; set; }
    public UnitTypeEnum UnitType { get; set; } = UnitTypeEnum.Un;
    public long GroupId { get; set; }
}