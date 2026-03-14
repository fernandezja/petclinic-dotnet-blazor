using PetClinic.Domain.Owners;
using PetClinic.Domain.Vets;
using Xunit;

namespace PetClinic.Domain.Tests;

public class OwnerTests
{
    [Fact]
    public void AddPet_NewPet_AddsToCollection()
    {
        var owner = CreateOwner();
        var pet = new Pet { Name = "Leo", Type = new PetType { Name = "cat" } };

        owner.AddPet(pet);

        Assert.Single(owner.Pets);
        Assert.Equal("Leo", owner.Pets[0].Name);
    }

    [Fact]
    public void AddPet_ExistingPet_DoesNotAdd()
    {
        var owner = CreateOwner();
        var pet = new Pet { Id = 1, Name = "Leo", Type = new PetType { Name = "cat" } };

        owner.AddPet(pet); // IsNew = false (Id = 1)

        Assert.Empty(owner.Pets);
    }

    [Fact]
    public void GetPet_ByName_ReturnsCorrectPet()
    {
        var owner = CreateOwner();
        var pet = new Pet { Name = "Samantha", Type = new PetType { Name = "cat" } };
        owner.AddPet(pet);

        var result = owner.GetPet("samantha"); // case-insensitive

        Assert.NotNull(result);
        Assert.Equal("Samantha", result.Name);
    }

    [Fact]
    public void GetPet_ByName_NotFound_ReturnsNull()
    {
        var owner = CreateOwner();

        var result = owner.GetPet("NonExistent");

        Assert.Null(result);
    }

    [Fact]
    public void AddVisit_ValidPetId_AddsVisit()
    {
        var owner = CreateOwner();
        var pet = new Pet { Name = "Leo", Type = new PetType { Name = "cat" } };
        owner.AddPet(pet);
        // Simulate saved pet (force non-zero id via reflection)
        typeof(Pet).GetProperty(nameof(Pet.Id))!.SetValue(pet, 5);

        var visit = new Visit { Description = "rabies shot" };
        owner.AddVisit(5, visit);

        Assert.Single(pet.Visits);
    }

    [Fact]
    public void AddVisit_InvalidPetId_ThrowsException()
    {
        var owner = CreateOwner();

        Assert.Throws<InvalidOperationException>(() =>
            owner.AddVisit(999, new Visit { Description = "test" }));
    }

    [Fact]
    public void FullName_ReturnsCombinedName()
    {
        var owner = CreateOwner();
        Assert.Equal("John Doe", owner.FullName);
    }

    private static Owner CreateOwner() => new()
    {
        FirstName = "John",
        LastName = "Doe",
        Address = "123 Main St",
        City = "Springfield",
        Telephone = "6085551234"
    };
}

public class PetTests
{
    [Fact]
    public void AddVisit_AddsToCollection()
    {
        var pet = new Pet { Name = "Leo", Type = new PetType { Name = "cat" } };
        var visit = new Visit { Description = "checkup" };

        pet.AddVisit(visit);

        Assert.Single(pet.Visits);
    }

    [Fact]
    public void Visits_ReturnsSortedByDate()
    {
        var pet = new Pet { Name = "Max", Type = new PetType { Name = "dog" } };
        pet.AddVisit(new Visit { Date = new DateOnly(2023, 5, 10), Description = "second" });
        pet.AddVisit(new Visit { Date = new DateOnly(2023, 1, 1), Description = "first" });

        var visits = pet.Visits;

        Assert.Equal("first", visits[0].Description);
        Assert.Equal("second", visits[1].Description);
    }
}

public class VetTests
{
    [Fact]
    public void AddSpecialty_AddsToCollection()
    {
        var vet = new Vet { FirstName = "James", LastName = "Carter" };
        var specialty = new Specialty { Name = "radiology" };

        vet.AddSpecialty(specialty);

        Assert.Equal(1, vet.NrOfSpecialties);
        Assert.Equal("radiology", vet.Specialties[0].Name);
    }

    [Fact]
    public void Specialties_ReturnsSortedByName()
    {
        var vet = new Vet { FirstName = "Linda", LastName = "Douglas" };
        vet.AddSpecialty(new Specialty { Name = "surgery" });
        vet.AddSpecialty(new Specialty { Name = "dentistry" });

        Assert.Equal("dentistry", vet.Specialties[0].Name);
        Assert.Equal("surgery", vet.Specialties[1].Name);
    }
}
