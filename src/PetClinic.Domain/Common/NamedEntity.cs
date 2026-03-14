namespace PetClinic.Domain.Common;

public abstract class NamedEntity : BaseEntity
{
    [Required, MaxLength(80)]
    public required string Name { get; set; }

    public override string ToString() => Name;
}
