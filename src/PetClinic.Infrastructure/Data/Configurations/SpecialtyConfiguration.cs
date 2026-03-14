using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinic.Domain.Vets;

namespace PetClinic.Infrastructure.Data.Configurations;

public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
{
    public void Configure(EntityTypeBuilder<Specialty> builder)
    {
        builder.ToTable("specialties");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(s => s.Name).HasColumnName("name").HasMaxLength(80).IsRequired();
        builder.HasIndex(s => s.Name).HasDatabaseName("specialties_name");
    }
}
