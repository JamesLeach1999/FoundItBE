using FluentValidation;
using FinderBE.Models;

namespace FinderBE.Validation;

public class NewUserRequestValidator : AbstractValidator<User>
{
    public NewUserRequestValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username must be provided");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password must be provided");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email must be provided");
    }
}
