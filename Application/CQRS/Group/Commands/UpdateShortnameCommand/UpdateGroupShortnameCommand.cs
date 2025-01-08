using MediatR;

namespace Application.CQRS.Group.Commands.UpdateShortnameCommand;

public class UpdateGroupShortnameCommand : IRequest
{
    public Guid RequesterId { get; set; }
    public Guid GroupId { get; set; }
    public string Shortname { get; set; }
}