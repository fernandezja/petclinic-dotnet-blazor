namespace PetClinic.Domain.Owners;

public interface IOwnerRepository
{
    Task<Owner?> FindByIdAsync(int id, CancellationToken ct = default);

    Task<(IReadOnlyList<Owner> Items, int TotalCount)> FindByLastNameStartingWithAsync(
        string lastName, int page, int pageSize, CancellationToken ct = default);

    Task SaveAsync(Owner owner, CancellationToken ct = default);
}
