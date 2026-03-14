using PetClinic.Application.Owners.Commands;
using PetClinic.Application.Owners.Queries;

namespace PetClinic.Application.Owners;

public interface IOwnerService
{
    Task<OwnerDto?> GetOwnerByIdAsync(int id, CancellationToken ct = default);
    Task<PagedResult<OwnerSummaryDto>> FindOwnersByLastNameAsync(FindOwnersByLastNameQuery query, CancellationToken ct = default);
    Task<int> CreateOwnerAsync(CreateOwnerCommand command, CancellationToken ct = default);
    Task UpdateOwnerAsync(UpdateOwnerCommand command, CancellationToken ct = default);
    Task AddPetAsync(AddPetCommand command, CancellationToken ct = default);
    Task UpdatePetAsync(UpdatePetCommand command, CancellationToken ct = default);
    Task AddVisitAsync(AddVisitCommand command, CancellationToken ct = default);
    Task<IReadOnlyList<PetTypeDto>> GetPetTypesAsync(CancellationToken ct = default);
}
