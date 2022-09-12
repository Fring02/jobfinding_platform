using System.ComponentModel.DataAnnotations.Schema;
using ISPH.Domain.Enums;
using ISPH.Domain.Models.Base;
using ISPH.Domain.Models.Users;

namespace ISPH.Domain.Models.Advertisements;

public class Advertisement : BaseEntity<Guid>
{
    public string Title { get; set; } = null!;
    public uint Salary { get; set; }
    public string Description { get; set; } = null!;
    public WorkTime WorkTimeType { get; set; }

    [NotMapped]
    public string? WorkTime => WorkTimeType switch
        {
            Enums.WorkTime.Undefined => "Not important",
            Enums.WorkTime.FullTime => "Full-time",
            Enums.WorkTime.PartTime => "Part-time",
            Enums.WorkTime.Individual => "Individual",
            _ => null
        };

    public EmploymentType EmploymentType { get; set; }
    [NotMapped]
    public string? Employment => EmploymentType == EmploymentType.Undefined ? "Not important" : Enum.GetName(EmploymentType);

    public DateTime PostedAt { get; set; } = DateTime.Now;
    [NotMapped]
    public string PostedDateToString => PostedAt.ToLongDateString();
    public Guid PositionId { get; set; }
    public Position Position { get; set; }
    public Guid EmployerId { get; set; }
    public Employer Employer { get; set; }
    public IEnumerable<FeaturedAdvertisement> FeaturedAdvertisements { get; set; }
    public IEnumerable<AdvertisementResponse> Responses { get; set; }
}