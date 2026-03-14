namespace PetClinic.Domain.Vets;

public class Vet : Person
{
    private readonly List<Specialty> _specialties = [];

    public IReadOnlyList<Specialty> Specialties =>
        [.. _specialties.OrderBy(s => s.Name)];

    public int NrOfSpecialties => _specialties.Count;

    public void AddSpecialty(Specialty specialty) => _specialties.Add(specialty);
}
