namespace PetClinic.Domain.Owners;

public class Owner : Person
{
    [Required, MaxLength(255)]
    public required string Address { get; set; }

    [Required, MaxLength(80)]
    public required string City { get; set; }

    [Required, RegularExpression(@"\d{10}", ErrorMessage = "Telephone must be a 10-digit number")]
    public required string Telephone { get; set; }

    private readonly List<Pet> _pets = [];
    public IReadOnlyList<Pet> Pets => _pets.AsReadOnly();

    public void AddPet(Pet pet)
    {
        if (pet.IsNew) _pets.Add(pet);
    }

    public Pet? GetPet(string name, bool ignoreNew = false) =>
        _pets.FirstOrDefault(p =>
            string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase)
            && (!ignoreNew || !p.IsNew));

    public Pet? GetPet(int id) =>
        _pets.FirstOrDefault(p => !p.IsNew && p.Id == id);

    public void AddVisit(int petId, Visit visit)
    {
        ArgumentNullException.ThrowIfNull(visit);
        var pet = GetPet(petId)
            ?? throw new InvalidOperationException($"Pet with id {petId} not found for this owner.");
        pet.AddVisit(visit);
    }
}
