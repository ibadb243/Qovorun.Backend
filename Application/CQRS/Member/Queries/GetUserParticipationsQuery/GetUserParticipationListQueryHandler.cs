using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Member.Queries.GetUserParticipationsQuery;

public class GetUserParticipationListQueryHandler : IRequestHandler<GetUserParticipationListQuery, UserParticipantListVm>
{
    private readonly IUserDbContext _userDbContext;
    private readonly IGroupDbContext _groupDbContext;
    private readonly IMemberDbContext _memberDbContext;
    private readonly IMapper _mapper;

    public GetUserParticipationListQueryHandler(IUserDbContext userDbContext, IGroupDbContext groupDbContext, IMemberDbContext memberDbContext, IMapper mapper)
    {
        _userDbContext = userDbContext;
        _groupDbContext = groupDbContext;
        _memberDbContext = memberDbContext;
        _mapper = mapper;
    }
    
    public async Task<UserParticipantListVm> Handle(GetUserParticipationListQuery request, CancellationToken cancellationToken)
    {
        var requester = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.RequesterId, cancellationToken);
        if (requester == null || requester.DeletedAt != null) throw new Exception("User not found");

        var participantList = await _memberDbContext.Members
            .Where(m => m.UserId == requester.Id && m.BannedAt == null)
            .Join(_groupDbContext.Groups,
                m => m.GroupId,
                g => g.Id,
                (m, g) => new MemberGroupDto(m, g))
            .ProjectTo<UserParticipantDto>(_mapper.ConfigurationProvider)
            .Skip(request.Offset)
            .Take(request.Limit)
            .ToListAsync(cancellationToken);

        return new UserParticipantListVm { Participants = participantList };
    }
}