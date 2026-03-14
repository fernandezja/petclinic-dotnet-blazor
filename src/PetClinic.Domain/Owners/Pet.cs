namespace PetClinic.Domain.Owners;

public class Pet : NamedEntity
{
    public DateOnly? BirthDate { get; set; }

    public int TypeId { get; set; }
    public PetType? Type { get; set; }

    public int OwnerId { get; set; }

    private readonly List<Visit> _visits = [];
    public IReadOnlyList<Visit> Visits => [.. _visits.OrderBy(v => v.Date)];

    public void AddVisit(Visit visit) => _visits.Add(visit);
}
