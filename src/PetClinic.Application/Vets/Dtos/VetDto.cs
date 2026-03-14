namespace PetClinic.Application.Vets.Dtos;

public record VetDto(int Id, string FirstName, string LastName, IReadOnlyList<string> Specialties);
