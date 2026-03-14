namespace PetClinic.Domain.Vets;

public interface IVetRepository
{
    Task<IReadOnlyList<Vet>> FindAllAsync(CancellationToken ct = default);

    Task<(IReadOnlyList<Vet> Items, int TotalCount)> FindPagedAsync(
        int page, int pageSize, CancellationToken ct = default);
}
