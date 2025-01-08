using MediatR;

namespace Application.CQRS.Group.Commands.DeleteCommand;

public class DeleteGroupCommand : IRequest
{
    public Guid RequesterId { get; set; }
    public Guid GroupId { get; set; }
}