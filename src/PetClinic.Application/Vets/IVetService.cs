namespace PetClinic.Application.Vets;

public interface IVetService
{
    Task<PagedResult<VetDto>> GetVetsPagedAsync(GetVetsPagedQuery query, CancellationToken ct = default);
    Task<IReadOnlyList<VetDto>> GetAllVetsAsync(CancellationToken ct = default);
}
