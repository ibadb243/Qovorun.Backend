namespace Application.CQRS.Member.Queries.GetUserParticipationsQuery;

internal record MemberGroupDto(Domain.Entities.Member Member, Domain.Entities.Group Group);