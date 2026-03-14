namespace PetClinic.Domain.Owners;

public interface IPetTypeRepository
{
    Task<IReadOnlyList<PetType>> FindAllOrderedByNameAsync(CancellationToken ct = default);
}
