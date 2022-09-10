using ISPH.Shared.Dtos.Companies;

namespace ISPH.Shared.Dtos.Users;

public record AdvertisementEmployerDto
{
    public Guid? Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Patronymic { get; set; }
    public string Email { get; set; }
    public CompanyItemDto Company { get; set; }
}