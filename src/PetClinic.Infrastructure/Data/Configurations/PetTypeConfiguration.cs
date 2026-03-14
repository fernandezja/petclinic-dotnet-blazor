using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetClinic.Infrastructure.Data.Configurations;

public class PetTypeConfiguration : IEntityTypeConfiguration<PetType>
{
    public void Configure(EntityTypeBuilder<PetType> builder)
    {
        builder.ToTable("types");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(80).IsRequired();
        builder.HasIndex(p => p.Name).HasDatabaseName("types_name");
    }
}
