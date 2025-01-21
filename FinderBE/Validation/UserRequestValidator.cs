using FluentValidation;

namespace FinderBE.Validation;

public class UserRequestValidator : AbstractValidator<Guid>
{
    public UserRequestValidator()
    {
        RuleFor(x => x.ToString())
            .NotEmpty().WithMessage("GUID is required.")
            .Must(BeAValidGuid).WithMessage("Invalid GUID format.");
    }

    private bool BeAValidGuid(string guidValue)
    {
        return Guid.TryParse(guidValue, out _);
    }
}