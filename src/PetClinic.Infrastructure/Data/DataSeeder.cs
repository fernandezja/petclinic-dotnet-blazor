using PetClinic.Domain.Vets;
using PetClinic.Infrastructure.Data;

namespace PetClinic.Infrastructure.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<PetClinicDbContext>();
        await db.Database.EnsureCreatedAsync();

        if (db.Vets.Any()) return; // Already seeded

        // Specialties
        var radiology = new Specialty { Name = "radiology" };
        var surgery = new Specialty { Name = "surgery" };
        var dentistry = new Specialty { Name = "dentistry" };
        db.Specialties.AddRange(radiology, surgery, dentistry);

        // Vets
        var carter = new Vet { FirstName = "James", LastName = "Carter" };
        var leary = new Vet { FirstName = "Helen", LastName = "Leary" };
        var douglas = new Vet { FirstName = "Linda", LastName = "Douglas" };
        var ortega = new Vet { FirstName = "Rafael", LastName = "Ortega" };
        var stevens = new Vet { FirstName = "Henry", LastName = "Stevens" };
        var jenkins = new Vet { FirstName = "Sharon", LastName = "Jenkins" };

        leary.AddSpecialty(radiology);
        douglas.AddSpecialty(surgery);
        douglas.AddSpecialty(dentistry);
        ortega.AddSpecialty(surgery);
        stevens.AddSpecialty(radiology);

        db.Vets.AddRange(carter, leary, douglas, ortega, stevens, jenkins);

        // Pet Types
        var cat = new PetType { Name = "cat" };
        var dog = new PetType { Name = "dog" };
        var lizard = new PetType { Name = "lizard" };
        var snake = new PetType { Name = "snake" };
        var bird = new PetType { Name = "bird" };
        var hamster = new PetType { Name = "hamster" };
        db.PetTypes.AddRange(cat, dog, lizard, snake, bird, hamster);

        // Owners & Pets
        var george = new Owner
        {
            FirstName = "George", LastName = "Franklin",
            Address = "110 W. Liberty St.", City = "Madison", Telephone = "6085551023"
        };
        george.AddPet(new Pet { Name = "Leo", BirthDate = new DateOnly(2010, 9, 7), Type = cat });

        var betty = new Owner
        {
            FirstName = "Betty", LastName = "Davis",
            Address = "638 Cardinal Ave.", City = "Sun Prairie", Telephone = "6085551749"
        };
        betty.AddPet(new Pet { Name = "Basil", BirthDate = new DateOnly(2012, 8, 6), Type = hamster });

        var eduardo = new Owner
        {
            FirstName = "Eduardo", LastName = "Rodriquez",
            Address = "2693 Commerce St.", City = "McFarland", Telephone = "6085558763"
        };
        eduardo.AddPet(new Pet { Name = "Rosy", BirthDate = new DateOnly(2011, 4, 17), Type = dog });
        eduardo.AddPet(new Pet { Name = "Jewel", BirthDate = new DateOnly(2010, 3, 7), Type = dog });

        var harold = new Owner
        {
            FirstName = "Harold", LastName = "Davis",
            Address = "563 Friendly St.", City = "Windsor", Telephone = "6085553198"
        };
        harold.AddPet(new Pet { Name = "Iggy", BirthDate = new DateOnly(2010, 11, 30), Type = lizard });

        var peter = new Owner
        {
            FirstName = "Peter", LastName = "McTavish",
            Address = "2387 S. Fair Way", City = "Madison", Telephone = "6085552765"
        };
        peter.AddPet(new Pet { Name = "George", BirthDate = new DateOnly(2010, 1, 20), Type = snake });

        var jean = new Owner
        {
            FirstName = "Jean", LastName = "Coleman",
            Address = "105 N. Lake St.", City = "Monona", Telephone = "6085552654"
        };
        var samantha = new Pet { Name = "Samantha", BirthDate = new DateOnly(2012, 9, 4), Type = cat };
        var max = new Pet { Name = "Max", BirthDate = new DateOnly(2012, 9, 4), Type = cat };
        samantha.AddVisit(new Visit { Date = new DateOnly(2013, 1, 1), Description = "rabies shot" });
        samantha.AddVisit(new Visit { Date = new DateOnly(2013, 1, 4), Description = "spayed" });
        max.AddVisit(new Visit { Date = new DateOnly(2013, 1, 2), Description = "rabies shot" });
        max.AddVisit(new Visit { Date = new DateOnly(2013, 1, 3), Description = "neutered" });
        jean.AddPet(samantha);
        jean.AddPet(max);

        var jeff = new Owner
        {
            FirstName = "Jeff", LastName = "Black",
            Address = "1450 Oak Blvd.", City = "Monona", Telephone = "6085555387"
        };
        jeff.AddPet(new Pet { Name = "Lucky", BirthDate = new DateOnly(2011, 8, 6), Type = bird });

        var maria = new Owner
        {
            FirstName = "Maria", LastName = "Escobito",
            Address = "345 Maple St.", City = "Madison", Telephone = "6085557683"
        };
        maria.AddPet(new Pet { Name = "Mulligan", BirthDate = new DateOnly(2007, 2, 24), Type = dog });

        var david = new Owner
        {
            FirstName = "David", LastName = "Schroeder",
            Address = "2749 Blackhawk Trail", City = "Madison", Telephone = "6085559435"
        };
        david.AddPet(new Pet { Name = "Freddy", BirthDate = new DateOnly(2010, 3, 9), Type = bird });

        var carlos = new Owner
        {
            FirstName = "Carlos", LastName = "Estaban",
            Address = "2335 Independence La.", City = "Waunakee", Telephone = "6085555487"
        };
        carlos.AddPet(new Pet { Name = "Lucky", BirthDate = new DateOnly(2010, 6, 24), Type = dog });
        carlos.AddPet(new Pet { Name = "Sly", BirthDate = new DateOnly(2012, 6, 8), Type = cat });

        db.Owners.AddRange(george, betty, eduardo, harold, peter, jean, jeff, maria, david, carlos);

        await db.SaveChangesAsync();
    }
}
