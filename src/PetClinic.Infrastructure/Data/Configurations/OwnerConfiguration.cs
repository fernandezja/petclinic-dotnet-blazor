using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetClinic.Infrastructure.Data.Configurations;

public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.ToTable("owners");
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(o => o.FirstName).HasColumnName("first_name").HasMaxLength(30).IsRequired();
        builder.Property(o => o.LastName).HasColumnName("last_name").HasMaxLength(30).IsRequired();
        builder.Property(o => o.Address).HasColumnName("address").HasMaxLength(255).IsRequired();
        builder.Property(o => o.City).HasColumnName("city").HasMaxLength(80).IsRequired();
        builder.Property(o => o.Telephone).HasColumnName("telephone").HasMaxLength(20).IsRequired();
        builder.HasIndex(o => o.LastName).HasDatabaseName("owners_last_name");

        builder.HasMany(o => o.Pets)
            .WithOne()
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(o => o.Pets)
            .HasField("_pets")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
