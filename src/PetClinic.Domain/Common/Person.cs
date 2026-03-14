namespace PetClinic.Domain.Common;

public abstract class Person : BaseEntity
{
    [Required, MaxLength(30)]
    public required string FirstName { get; set; }

    [Required, MaxLength(30)]
    public required string LastName { get; set; }

    public string FullName => $"{FirstName} {LastName}";
}
