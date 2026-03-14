namespace PetClinic.Infrastructure.Data.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(30).IsRequired();
        builder.Property(p => p.BirthDate).HasColumnName("birth_date");
        builder.Property(p => p.TypeId).HasColumnName("type_id");
        builder.Property(p => p.OwnerId).HasColumnName("owner_id");
        builder.HasIndex(p => p.Name).HasDatabaseName("pets_name");

        builder.HasOne(p => p.Type)
            .WithMany()
            .HasForeignKey(p => p.TypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Visits)
            .WithOne()
            .HasForeignKey(v => v.PetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(p => p.Visits)
            .HasField("_visits")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
