using MediatR;

namespace Application.CQRS.Member.Commands.DeleteCommand;

public class BanMemberCommand : IRequest
{
    public Guid RequesterId { get; set; }
    public Guid GroupId { get; set; } 
    public Guid MemberId { get; set; }
}