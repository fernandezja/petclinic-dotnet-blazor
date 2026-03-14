using PetClinic.Infrastructure.Data;

namespace PetClinic.Infrastructure.Repositories;

public class PetTypeRepository(PetClinicDbContext db) : IPetTypeRepository
{
    public async Task<IReadOnlyList<PetType>> FindAllOrderedByNameAsync(CancellationToken ct = default) =>
        await db.PetTypes
            .OrderBy(t => t.Name)
            .ToListAsync(ct);
}
