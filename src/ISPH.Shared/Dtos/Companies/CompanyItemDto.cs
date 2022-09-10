namespace ISPH.Shared.Dtos.Companies;

public record CompanyItemDto : IDto<Guid>
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
}