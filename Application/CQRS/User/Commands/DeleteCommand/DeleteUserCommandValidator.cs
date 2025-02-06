using FluentValidation;

namespace Application.CQRS.User.Commands.DeleteCommand;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.RequesterId).NotEqual(Guid.Empty);
    }
}