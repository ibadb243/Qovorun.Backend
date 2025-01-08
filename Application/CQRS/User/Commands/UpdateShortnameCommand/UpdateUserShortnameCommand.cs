using MediatR;

namespace Application.CQRS.User.Commands.UpdateShortnameCommand;

public class UpdateUserShortnameCommand : IRequest
{
    public Guid RequesterId { get; set; }
    public string Shortname { get; set; }
}