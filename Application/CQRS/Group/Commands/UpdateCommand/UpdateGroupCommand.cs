using MediatR;

namespace Application.CQRS.Group.Commands.UpdateCommand;

public class UpdateGroupCommand : IRequest
{
    public Guid RequesterId { get; set; }
    public Guid GroupId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}