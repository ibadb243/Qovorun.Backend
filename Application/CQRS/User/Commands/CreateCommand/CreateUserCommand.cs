using MediatR;

namespace Application.CQRS.User.Commands.CreateCommand;

public class CreateUserCommand : IRequest<Guid>
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Shortname { get; set; }
    public string Description { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}