using MediatR;

namespace Application.CQRS.User.Commands.DeleteCommand;

public class DeleteUserCommand : IRequest
{
    public Guid RequesterId { get; set; }
}