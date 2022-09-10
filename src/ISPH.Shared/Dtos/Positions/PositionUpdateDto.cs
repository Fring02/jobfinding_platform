namespace ISPH.Shared.Dtos.Positions;

public record PositionUpdateDto : IDto<Guid>
{
    public string? Name { get; set; }
    public string? Path { get; set; }
    public string? Description { get; set; }
    [JsonIgnore]
    public Guid? Id { get; set; }
}