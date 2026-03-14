namespace PetClinic.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public bool IsNew => Id == 0;
}
