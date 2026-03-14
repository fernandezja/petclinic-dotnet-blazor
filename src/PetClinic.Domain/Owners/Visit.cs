namespace PetClinic.Domain.Owners;

public class Visit : BaseEntity
{
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    [Required, MaxLength(255)]
    public required string Description { get; set; }

    public int PetId { get; set; }
}
