using FluentValidation;

namespace Application.CQRS.User.Commands.CreateCommand;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Firstname).NotEmpty().MinimumLength(4).MaximumLength(64);
        RuleFor(x => x.Lastname).MaximumLength(64);
        RuleFor(x => x.Shortname).NotEmpty().MinimumLength(4).MaximumLength(128);
        RuleFor(x => x.Description).MaximumLength(4024);
        RuleFor(x => x.PhoneNumber).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(64);
    }
}