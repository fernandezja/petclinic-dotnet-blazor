namespace PetClinic.Application.Owners.Validators;

public class CreateOwnerCommandValidator : AbstractValidator<CreateOwnerCommand>
{
    public CreateOwnerCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(30);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(30);
        RuleFor(x => x.Address).NotEmpty().MaximumLength(255);
        RuleFor(x => x.City).NotEmpty().MaximumLength(80);
        RuleFor(x => x.Telephone).NotEmpty().Matches(@"^\d{10}$")
            .WithMessage("Telephone must be a 10-digit number");
    }
}

public class UpdateOwnerCommandValidator : AbstractValidator<UpdateOwnerCommand>
{
    public UpdateOwnerCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(30);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(30);
        RuleFor(x => x.Address).NotEmpty().MaximumLength(255);
        RuleFor(x => x.City).NotEmpty().MaximumLength(80);
        RuleFor(x => x.Telephone).NotEmpty().Matches(@"^\d{10}$")
            .WithMessage("Telephone must be a 10-digit number");
    }
}

public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
{
    public AddPetCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(30);
        RuleFor(x => x.BirthDate).NotNull()
            .Must(d => d <= DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Birth date cannot be in the future");
        RuleFor(x => x.TypeId).GreaterThan(0).WithMessage("Type is required");
    }
}

public class UpdatePetCommandValidator : AbstractValidator<UpdatePetCommand>
{
    public UpdatePetCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(30);
        RuleFor(x => x.BirthDate).NotNull()
            .Must(d => d <= DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Birth date cannot be in the future");
    }
}

public class AddVisitCommandValidator : AbstractValidator<AddVisitCommand>
{
    public AddVisitCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Date).NotEmpty();
    }
}
