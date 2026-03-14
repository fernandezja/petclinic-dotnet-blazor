namespace PetClinic.Infrastructure.Data.Configurations;

public class VetConfiguration : IEntityTypeConfiguration<Vet>
{
    public void Configure(EntityTypeBuilder<Vet> builder)
    {
        builder.ToTable("vets");
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(v => v.FirstName).HasColumnName("first_name").HasMaxLength(30).IsRequired();
        builder.Property(v => v.LastName).HasColumnName("last_name").HasMaxLength(30).IsRequired();
        builder.HasIndex(v => v.LastName).HasDatabaseName("vets_last_name");

        builder.HasMany(v => v.Specialties)
            .WithMany()
            .UsingEntity(j => j.ToTable("vet_specialties"));

        builder.Navigation(v => v.Specialties)
            .HasField("_specialties")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
