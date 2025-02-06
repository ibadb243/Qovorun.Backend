using MediatR;

namespace Application.CQRS.Group.Commands.CreateCommand;

public class CreateGroupCommand : IRequest<Guid>
{
    public Guid RequesterId { get; set; }
    public string Name { get; set; }
    public string Shortname { get; set; }
    public string Description { get; set; }
}