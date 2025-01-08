using MediatR;

namespace Application.CQRS.Member.Queries.GetUserParticipationsQuery;

public class GetUserParticipationListQuery : IRequest<UserParticipantListVm>
{
    public Guid RequesterId { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; }
}