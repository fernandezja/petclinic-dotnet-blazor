namespace PetClinic.Application.Owners.Commands;

public record CreateOwnerCommand(
    string FirstName, string LastName,
    string Address, string City, string Telephone);

public record UpdateOwnerCommand(
    int Id, string FirstName, string LastName,
    string Address, string City, string Telephone);

public record AddPetCommand(
    int OwnerId, string Name, DateOnly? BirthDate, int TypeId);

public record UpdatePetCommand(
    int OwnerId, int PetId, string Name, DateOnly? BirthDate, int TypeId);

public record AddVisitCommand(
    int OwnerId, int PetId, DateOnly Date, string Description);
