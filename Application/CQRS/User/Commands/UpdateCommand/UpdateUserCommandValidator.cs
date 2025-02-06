using FluentValidation;

namespace Application.CQRS.User.Commands.UpdateCommand;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.RequesterId).NotEqual(Guid.Empty);
        RuleFor(x => x.Firstname).NotEmpty().MinimumLength(4).MaximumLength(64);
        RuleFor(x => x.Lastname).MaximumLength(64);
        RuleFor(x => x.Description).MaximumLength(4024);
    }
}