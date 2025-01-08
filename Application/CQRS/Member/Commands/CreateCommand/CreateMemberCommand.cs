using MediatR;

namespace Application.CQRS.Member.Commands.CreateCommand;

public class CreateMemberCommand : IRequest<Guid>
{
    public Guid RequesterId { get; set; }
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
}