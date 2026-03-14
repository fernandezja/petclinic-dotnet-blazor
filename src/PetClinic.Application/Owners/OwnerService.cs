namespace PetClinic.Application.Owners;

public class OwnerService(IOwnerRepository ownerRepository, IPetTypeRepository petTypeRepository) : IOwnerService
{
    public async Task<OwnerDto?> GetOwnerByIdAsync(int id, CancellationToken ct = default)
    {
        var owner = await ownerRepository.FindByIdAsync(id, ct);
        return owner is null ? null : MapToDto(owner);
    }

    public async Task<PagedResult<OwnerSummaryDto>> FindOwnersByLastNameAsync(
        FindOwnersByLastNameQuery query, CancellationToken ct = default)
    {
        var (items, total) = await ownerRepository.FindByLastNameStartingWithAsync(
            query.LastName, query.Page, query.PageSize, ct);

        var dtos = items.Select(o => new OwnerSummaryDto(
            o.Id, o.FirstName, o.LastName, o.Address, o.City, o.Telephone,
            [.. o.Pets.Select(p => p.Name)])).ToList();

        return new PagedResult<OwnerSummaryDto>(dtos, total, query.Page, query.PageSize);
    }

    public async Task<int> CreateOwnerAsync(CreateOwnerCommand command, CancellationToken ct = default)
    {
        var owner = new Owner
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Address = command.Address,
            City = command.City,
            Telephone = command.Telephone
        };
        await ownerRepository.SaveAsync(owner, ct);
        return owner.Id;
    }

    public async Task UpdateOwnerAsync(UpdateOwnerCommand command, CancellationToken ct = default)
    {
        var owner = await ownerRepository.FindByIdAsync(command.Id, ct)
            ?? throw new EntityNotFoundException(nameof(Owner), command.Id);

        owner.FirstName = command.FirstName;
        owner.LastName = command.LastName;
        owner.Address = command.Address;
        owner.City = command.City;
        owner.Telephone = command.Telephone;

        await ownerRepository.SaveAsync(owner, ct);
    }

    public async Task AddPetAsync(AddPetCommand command, CancellationToken ct = default)
    {
        var owner = await ownerRepository.FindByIdAsync(command.OwnerId, ct)
            ?? throw new EntityNotFoundException(nameof(Owner), command.OwnerId);

        var petType = (await petTypeRepository.FindAllOrderedByNameAsync(ct))
            .FirstOrDefault(t => t.Id == command.TypeId)
            ?? throw new EntityNotFoundException(nameof(PetType), command.TypeId);

        var pet = new Pet { Name = command.Name, BirthDate = command.BirthDate, Type = petType };
        owner.AddPet(pet);
        await ownerRepository.SaveAsync(owner, ct);
    }

    public async Task UpdatePetAsync(UpdatePetCommand command, CancellationToken ct = default)
    {
        var owner = await ownerRepository.FindByIdAsync(command.OwnerId, ct)
            ?? throw new EntityNotFoundException(nameof(Owner), command.OwnerId);

        var pet = owner.GetPet(command.PetId)
            ?? throw new EntityNotFoundException(nameof(Pet), command.PetId);

        var petType = (await petTypeRepository.FindAllOrderedByNameAsync(ct))
            .FirstOrDefault(t => t.Id == command.TypeId)
            ?? throw new EntityNotFoundException(nameof(PetType), command.TypeId);

        pet.Name = command.Name;
        pet.BirthDate = command.BirthDate;
        pet.Type = petType;

        await ownerRepository.SaveAsync(owner, ct);
    }

    public async Task AddVisitAsync(AddVisitCommand command, CancellationToken ct = default)
    {
        var owner = await ownerRepository.FindByIdAsync(command.OwnerId, ct)
            ?? throw new EntityNotFoundException(nameof(Owner), command.OwnerId);

        var visit = new Visit { Date = command.Date, Description = command.Description };
        owner.AddVisit(command.PetId, visit);
        await ownerRepository.SaveAsync(owner, ct);
    }

    public async Task<IReadOnlyList<PetTypeDto>> GetPetTypesAsync(CancellationToken ct = default)
    {
        var types = await petTypeRepository.FindAllOrderedByNameAsync(ct);
        return [.. types.Select(t => new PetTypeDto(t.Id, t.Name))];
    }

    // --- Mapping helpers ---

    private static OwnerDto MapToDto(Owner owner) => new(
        owner.Id, owner.FirstName, owner.LastName,
        owner.Address, owner.City, owner.Telephone,
        [.. owner.Pets.Select(MapPetToDto)]);

    private static PetDto MapPetToDto(Pet pet) => new(
        pet.Id, pet.Name, pet.BirthDate,
        pet.Type?.Name ?? string.Empty,
        [.. pet.Visits.Select(v => new VisitDto(v.Id, v.Date, v.Description))]);
}
