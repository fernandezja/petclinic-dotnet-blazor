using PetClinic.Application.Vets.Dtos;
using PetClinic.Application.Vets.Queries;

namespace PetClinic.Application.Vets;

public class VetService(IVetRepository vetRepository) : IVetService
{
    public async Task<IReadOnlyList<VetDto>> GetAllVetsAsync(CancellationToken ct = default)
    {
        var vets = await vetRepository.FindAllAsync(ct);
        return [.. vets.Select(MapToDto)];
    }

    public async Task<PagedResult<VetDto>> GetVetsPagedAsync(
        GetVetsPagedQuery query, CancellationToken ct = default)
    {
        var (items, total) = await vetRepository.FindPagedAsync(query.Page, query.PageSize, ct);
        return new PagedResult<VetDto>([.. items.Select(MapToDto)], total, query.Page, query.PageSize);
    }

    private static VetDto MapToDto(Vet vet) =>
        new(vet.Id, vet.FirstName, vet.LastName,
            [.. vet.Specialties.Select(s => s.Name)]);
}
