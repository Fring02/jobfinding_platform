namespace ISPH.Shared.Dtos.Users;

public record EmployerItemDto : IDto<Guid>
{
    public Guid? Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Patronymic { get; set; }
    public string Email { get; set; }
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; }
}