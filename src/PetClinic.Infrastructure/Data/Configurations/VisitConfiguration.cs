namespace PetClinic.Infrastructure.Data.Configurations;

public class VisitConfiguration : IEntityTypeConfiguration<Visit>
{
    public void Configure(EntityTypeBuilder<Visit> builder)
    {
        builder.ToTable("visits");
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(v => v.PetId).HasColumnName("pet_id");
        builder.Property(v => v.Date).HasColumnName("visit_date");
        builder.Property(v => v.Description).HasColumnName("description").HasMaxLength(255).IsRequired();
        builder.HasIndex(v => v.PetId).HasDatabaseName("visits_pet_id");
    }
}
