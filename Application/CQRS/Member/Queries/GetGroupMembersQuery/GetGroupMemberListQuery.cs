using MediatR;

namespace Application.CQRS.Member.Queries.GetGroupMembersQuery;

public class GetGroupMemberListQuery : IRequest<MemberListVm>
{
    public Guid RequesterId { get; set; }
    public Guid GroupId { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; }
}