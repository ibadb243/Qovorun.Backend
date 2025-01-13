using Application.Interfaces;
using Application.Interfaces.Contexts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Member.Queries.GetGroupMembersQuery;

public class GetGroupMemberListQueryHandler : IRequestHandler<GetGroupMemberListQuery, MemberListVm>
{
    private readonly IUserDbContext _userDbContext;
    private readonly IGroupDbContext _groupDbContext;
    private readonly IMemberDbContext _memberDbContext;
    private readonly IMapper _mapper;

    public GetGroupMemberListQueryHandler(IUserDbContext userDbContext, IGroupDbContext groupDbContext, IMemberDbContext memberDbContext, IMapper mapper)
    {
        _userDbContext = userDbContext;
        _groupDbContext = groupDbContext;
        _memberDbContext = memberDbContext;
        _mapper = mapper;
    }
    
    public async Task<MemberListVm> Handle(GetGroupMemberListQuery request, CancellationToken cancellationToken)
    {
        var requester = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.RequesterId, cancellationToken);
        if (requester == null || requester.DeletedAt != null) throw new Exception("User not found");
        
        var group = await _groupDbContext.Groups.FirstOrDefaultAsync(g => g.Id == request.GroupId, cancellationToken);
        if (group == null || group.DeletedAt != null) throw new Exception("Group not found");
        
        var requester_member = await _memberDbContext.Members.FirstOrDefaultAsync(m => m.UserId == requester.Id && m.GroupId == group.Id, cancellationToken);
        if (requester_member == null || requester_member.BannedAt != null) throw new Exception("You are not member of this group");
        
        var memberList = await _memberDbContext.Members
            .Where(m => m.GroupId == group.Id && m.BannedAt == null)
            .ProjectTo<MemberLookupDto>(_mapper.ConfigurationProvider)
            .Skip(request.Offset)
            .Take(request.Limit)
            .ToListAsync(cancellationToken);
        
        return new MemberListVm { Members = memberList };
    }
}