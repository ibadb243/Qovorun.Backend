using FluentValidation;

namespace Application.CQRS.Group.Commands.UpdateCommand;

public class UpdateGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
{
    public UpdateGroupCommandValidator()
    {
        RuleFor(x => x.RequesterId).NotEqual(Guid.Empty);
        RuleFor(x => x.GroupId).NotEqual(Guid.Empty);
        RuleFor(cmd => cmd.Name).NotEmpty().MinimumLength(4).MaximumLength(128);
        RuleFor(cmd => cmd.Description).MaximumLength(4024);
    }
}