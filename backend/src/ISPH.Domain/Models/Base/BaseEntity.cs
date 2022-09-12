using System.ComponentModel.DataAnnotations;

namespace ISPH.Domain.Models.Base;

public class BaseEntity<TId>
{
    [Key]
    public TId Id { get; set; }
}