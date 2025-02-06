using FluentValidation;

namespace Application.CQRS.Group.Commands.DeleteCommand;

public class DeleteGroupCommandValidator : AbstractValidator<DeleteGroupCommand>
{
    public DeleteGroupCommandValidator()
    {
        RuleFor(x => x.RequesterId).NotEqual(Guid.Empty);
        RuleFor(x => x.GroupId).NotEqual(Guid.Empty);
    }
}