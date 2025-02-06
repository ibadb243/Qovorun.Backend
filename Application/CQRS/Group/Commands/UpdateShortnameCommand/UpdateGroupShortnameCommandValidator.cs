using FluentValidation;

namespace Application.CQRS.Group.Commands.UpdateShortnameCommand;

public class UpdateGroupShortnameCommandValidator : AbstractValidator<UpdateGroupShortnameCommand>
{
    public UpdateGroupShortnameCommandValidator()
    {
        RuleFor(x => x.RequesterId).NotEqual(Guid.Empty);
        RuleFor(x => x.GroupId).NotEqual(Guid.Empty);
        RuleFor(x => x.Shortname).NotEmpty().MinimumLength(4).MaximumLength(128);
    }
}