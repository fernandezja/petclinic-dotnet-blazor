using PetClinic.Domain.Vets;

namespace PetClinic.Infrastructure.Data;

public class PetClinicDbContext(DbContextOptions<PetClinicDbContext> options) : DbContext(options)
{
    public DbSet<Owner> Owners => Set<Owner>();
    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<PetType> PetTypes => Set<PetType>();
    public DbSet<Visit> Visits => Set<Visit>();
    public DbSet<Vet> Vets => Set<Vet>();
    public DbSet<Specialty> Specialties => Set<Specialty>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PetClinicDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
