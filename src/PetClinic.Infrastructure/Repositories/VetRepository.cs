using Microsoft.Extensions.Options;
using PetClinic.Domain.Vets;
using PetClinic.Infrastructure.Data;

namespace PetClinic.Infrastructure.Repositories;

public class VetRepository(PetClinicDbContext db, IMemoryCache cache, IOptions<CacheOptions> cacheOptions) : IVetRepository
{
    private const string VetsCacheKey = "vets";

    public async Task<IReadOnlyList<Vet>> FindAllAsync(CancellationToken ct = default) =>
        await cache.GetOrCreateAsync(VetsCacheKey, async entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(cacheOptions.Value.VetsCacheDurationMinutes);
            return (IReadOnlyList<Vet>)await db.Vets
                .Include(v => v.Specialties)
                .OrderBy(v => v.LastName)
                .ToListAsync(ct);
        }) ?? [];

    public async Task<(IReadOnlyList<Vet> Items, int TotalCount)> FindPagedAsync(
        int page, int pageSize, CancellationToken ct = default)
    {
        var all = await FindAllAsync(ct);
        var total = all.Count;
        var items = all.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return (items, total);
    }
}

public class CacheOptions
{
    public double VetsCacheDurationMinutes { get; set; } = 60;
}
