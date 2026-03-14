using PetClinic.Infrastructure.Data;

namespace PetClinic.Infrastructure.Repositories;

public class OwnerRepository(PetClinicDbContext db) : IOwnerRepository
{
    public async Task<Owner?> FindByIdAsync(int id, CancellationToken ct = default) =>
        await db.Owners
            .Include(o => o.Pets).ThenInclude(p => p.Type)
            .Include(o => o.Pets).ThenInclude(p => (p as Pet)!.Visits)
            .FirstOrDefaultAsync(o => o.Id == id, ct);

    public async Task<(IReadOnlyList<Owner> Items, int TotalCount)> FindByLastNameStartingWithAsync(
        string lastName, int page, int pageSize, CancellationToken ct = default)
    {
        var query = db.Owners
            .Include(o => o.Pets)
            .Where(o => o.LastName.StartsWith(lastName))
            .OrderBy(o => o.LastName);

        var total = await query.CountAsync(ct);
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return (items, total);
    }

    public async Task SaveAsync(Owner owner, CancellationToken ct = default)
    {
        if (owner.IsNew)
            db.Owners.Add(owner);
        else
            db.Owners.Update(owner);

        await db.SaveChangesAsync(ct);
    }
}
