using FluentValidation;

namespace Application.CQRS.User.Commands.UpdateShortnameCommand;

public class UpdateUserShortnameCommandValidator : AbstractValidator<UpdateUserShortnameCommand>
{
    public UpdateUserShortnameCommandValidator()
    {
        RuleFor(x => x.RequesterId).NotEqual(Guid.Empty);
        RuleFor(x => x.Shortname).NotEmpty().MinimumLength(4).MaximumLength(128);
    }
}