namespace ISPH.Shared.Dtos.Positions;

public record PositionCreateDto : IDto<Guid>
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Path { get; set; }
    public string Description { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Guid? Id { get; set; }
}