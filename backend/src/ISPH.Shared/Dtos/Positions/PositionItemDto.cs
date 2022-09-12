namespace ISPH.Shared.Dtos.Positions;

public record PositionItemDto : IDto<Guid>
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
    public string Path { get; set; }
}