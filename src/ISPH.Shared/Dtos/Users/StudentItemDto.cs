namespace ISPH.Shared.Dtos.Users;

public record StudentItemDto : IDto<Guid>
{
    public Guid? Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Patronymic { get; set; }
    public string Email { get; set; }
    public string University { get; set; }
    public string Faculty { get; set; }
    public ushort Course { get; set; }
}