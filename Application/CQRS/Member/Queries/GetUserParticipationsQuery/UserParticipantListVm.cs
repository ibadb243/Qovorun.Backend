namespace Application.CQRS.Member.Queries.GetUserParticipationsQuery;

public class UserParticipantListVm
{
    public IList<UserParticipantDto> Participants { get; set; }
}