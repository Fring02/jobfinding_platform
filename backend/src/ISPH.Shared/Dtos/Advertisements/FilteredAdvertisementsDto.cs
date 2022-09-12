using System.Collections;
namespace ISPH.Shared.Dtos.Advertisements;

public record FilteredAdvertisementsDto : IReadOnlyCollection<AdvertisementItemDto>
{
    private readonly IEnumerable<AdvertisementItemDto> _results;
    public FilteredAdvertisementsDto(IEnumerable<AdvertisementItemDto> results, int count, uint maxSalary)
    {
        _results = results;
        Count = count;
        MaxSalary = maxSalary;
    }
    public uint MaxSalary { get; }
    public IEnumerator<AdvertisementItemDto> GetEnumerator() => _results.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public int Count { get; }

    public void Deconstruct(out IEnumerable<AdvertisementItemDto> results, out uint maxSalary, out int count)
    {
        results = _results;
        maxSalary = MaxSalary;
        count = Count;
    }
}