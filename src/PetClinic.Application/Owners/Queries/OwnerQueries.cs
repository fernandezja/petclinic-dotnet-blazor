namespace PetClinic.Application.Owners.Queries;

public record FindOwnersByLastNameQuery(string LastName, int Page = 1, int PageSize = 5);
