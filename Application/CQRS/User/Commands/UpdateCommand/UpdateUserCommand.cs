using MediatR;

namespace Application.CQRS.User.Commands.UpdateCommand;

public class UpdateUserCommand : IRequest
{
    public Guid RequesterId { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Description { get; set; }
}