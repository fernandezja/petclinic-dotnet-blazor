using FluentValidation;
using Moq;
using PetClinic.Application.Owners;
using PetClinic.Application.Owners.Commands;
using PetClinic.Application.Owners.Queries;
using PetClinic.Application.Owners.Validators;
using PetClinic.Domain.Owners;
using Xunit;

namespace PetClinic.Application.Tests;

public class OwnerServiceTests
{
    private readonly Mock<IOwnerRepository> _ownerRepoMock = new();
    private readonly Mock<IPetTypeRepository> _petTypeRepoMock = new();

    private OwnerService CreateService() => new(_ownerRepoMock.Object, _petTypeRepoMock.Object);

    [Fact]
    public async Task GetOwnerByIdAsync_ExistingOwner_ReturnsDto()
    {
        var owner = new Owner
        {
            FirstName = "George", LastName = "Franklin",
            Address = "110 W. Liberty", City = "Madison", Telephone = "6085551023"
        };
        typeof(Owner).GetProperty(nameof(Owner.Id))!.SetValue(owner, 1);

        _ownerRepoMock.Setup(r => r.FindByIdAsync(1, default)).ReturnsAsync(owner);

        var service = CreateService();
        var result = await service.GetOwnerByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("George", result.FirstName);
        Assert.Equal("Franklin", result.LastName);
    }

    [Fact]
    public async Task GetOwnerByIdAsync_NotFound_ReturnsNull()
    {
        _ownerRepoMock.Setup(r => r.FindByIdAsync(99, default)).ReturnsAsync((Owner?)null);

        var service = CreateService();
        var result = await service.GetOwnerByIdAsync(99);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateOwnerAsync_ValidCommand_CallsRepository()
    {
        _ownerRepoMock.Setup(r => r.SaveAsync(It.IsAny<Owner>(), default))
            .Returns(Task.CompletedTask);

        var service = CreateService();
        var cmd = new CreateOwnerCommand("John", "Doe", "123 St", "City", "6085551234");
        await service.CreateOwnerAsync(cmd);

        _ownerRepoMock.Verify(r => r.SaveAsync(It.IsAny<Owner>(), default), Times.Once);
    }

    [Fact]
    public async Task FindOwnersByLastNameAsync_ReturnsPaged()
    {
        var owners = new List<Owner>
        {
            new() { FirstName = "A", LastName = "Davis", Address = "1", City = "C", Telephone = "1234567890" },
            new() { FirstName = "B", LastName = "Davis", Address = "2", City = "C", Telephone = "1234567890" }
        };

        _ownerRepoMock.Setup(r => r.FindByLastNameStartingWithAsync("Davis", 1, 5, default))
            .ReturnsAsync(((IReadOnlyList<Owner>)owners, 2));

        var service = CreateService();
        var result = await service.FindOwnersByLastNameAsync(new FindOwnersByLastNameQuery("Davis"));

        Assert.Equal(2, result.TotalCount);
        Assert.Equal(2, result.Items.Count);
    }
}

public class ValidatorTests
{
    [Fact]
    public async Task CreateOwnerValidator_ValidData_Passes()
    {
        var validator = new CreateOwnerCommandValidator();
        var cmd = new CreateOwnerCommand("John", "Doe", "123 St", "City", "6085551234");

        var result = await validator.ValidateAsync(cmd);

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("", "Doe", "123 St", "City", "6085551234")]
    [InlineData("John", "", "123 St", "City", "6085551234")]
    [InlineData("John", "Doe", "123 St", "City", "12345")]     // too short phone
    [InlineData("John", "Doe", "123 St", "City", "abcdefghij")] // non-numeric phone
    public async Task CreateOwnerValidator_InvalidData_Fails(
        string fn, string ln, string addr, string city, string tel)
    {
        var validator = new CreateOwnerCommandValidator();
        var cmd = new CreateOwnerCommand(fn, ln, addr, city, tel);

        var result = await validator.ValidateAsync(cmd);

        Assert.False(result.IsValid);
    }

    [Fact]
    public async Task AddPetValidator_FutureBirthDate_Fails()
    {
        var validator = new AddPetCommandValidator();
        var cmd = new AddPetCommand(1, "Max", DateOnly.FromDateTime(DateTime.Today.AddDays(1)), 1);

        var result = await validator.ValidateAsync(cmd);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "BirthDate");
    }

    [Fact]
    public async Task AddPetValidator_MissingType_Fails()
    {
        var validator = new AddPetCommandValidator();
        var cmd = new AddPetCommand(1, "Max", DateOnly.FromDateTime(DateTime.Today), 0);

        var result = await validator.ValidateAsync(cmd);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "TypeId");
    }
}
