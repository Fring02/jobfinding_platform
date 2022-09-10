namespace ISPH.Shared.Dtos.Interfaces;

public interface IDto<TId> where TId : struct
{
    public TId? Id { get; set; }
}