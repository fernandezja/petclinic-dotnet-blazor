namespace PetClinic.Application.Owners.Dtos;

public record VisitDto(int Id, DateOnly Date, string Description);

public record PetTypeDto(int Id, string Name);

public record PetDto(
    int Id,
    string Name,
    DateOnly? BirthDate,
    string TypeName,
    IReadOnlyList<VisitDto> Visits);

public record OwnerDto(
    int Id,
    string FirstName,
    string LastName,
    string Address,
    string City,
    string Telephone,
    IReadOnlyList<PetDto> Pets);

public record OwnerSummaryDto(
    int Id,
    string FirstName,
    string LastName,
    string Address,
    string City,
    string Telephone,
    IReadOnlyList<string> PetNames);
