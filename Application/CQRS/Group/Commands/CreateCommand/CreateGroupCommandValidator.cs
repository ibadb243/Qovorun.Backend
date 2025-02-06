using FluentValidation;

namespace Application.CQRS.Group.Commands.CreateCommand;

public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(cmd => cmd.RequesterId).NotEqual(Guid.Empty);
        RuleFor(cmd => cmd.Shortname).NotEmpty().MinimumLength(4).MaximumLength(128);
        RuleFor(cmd => cmd.Name).NotEmpty().MinimumLength(4).MaximumLength(128);
        RuleFor(cmd => cmd.Description).MaximumLength(4024);
    }
}