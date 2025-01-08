namespace Application.CQRS.Member.Queries.GetGroupMembersQuery;

public class MemberListVm
{
    public IList<MemberLookupDto> Members { get; set; }
}