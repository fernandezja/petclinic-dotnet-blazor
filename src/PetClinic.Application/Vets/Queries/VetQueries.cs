namespace PetClinic.Application.Vets.Queries;

public record GetVetsPagedQuery(int Page = 1, int PageSize = 5);
