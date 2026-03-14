using PetClinic.Application.Vets.Dtos;
using PetClinic.Application.Vets.Queries;

namespace PetClinic.Application.Vets;

public interface IVetService
{
    Task<PagedResult<VetDto>> GetVetsPagedAsync(GetVetsPagedQuery query, CancellationToken ct = default);
    Task<IReadOnlyList<VetDto>> GetAllVetsAsync(CancellationToken ct = default);
}
