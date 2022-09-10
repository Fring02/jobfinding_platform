using System.ComponentModel.DataAnnotations.Schema;
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
    public string? WorkTime
    {
        get
        {
            var name = Enum.GetName(WorkTimeType);
            return name switch
            {
                "Undefined" => "Not important",
                "FullTime" => "Full-time",
                "PartTime" => "Part-time",
                _ => name
            };
        }
    }
    public EmploymentType EmploymentType { get; set; }
    [NotMapped]
    public string? Employment {
        get
        {
           var name = Enum.GetName(EmploymentType);
           return name == "Undefined" ? "Not important" : name;
        }
    }
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

public enum WorkTime { Undefined, FullTime, PartTime, Individual }
public enum EmploymentType { Undefined, Office, Remote }