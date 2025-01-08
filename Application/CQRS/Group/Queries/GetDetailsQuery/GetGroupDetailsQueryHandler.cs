using Application.Interfaces;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Group.Queries.GetDetailsQuery;

public class GetGroupDetailsQueryHandler : IRequestHandler<GetGroupDetailsQuery, GroupDetailsVm>
{
    private readonly IUserDbContext _userDbContext;
    private readonly IGroupDbContext _groupDbContext;
    private readonly IShortnameDbContext _shortnameDbContext;
    private readonly IMapper _mapper;
    
    public GetGroupDetailsQueryHandler(IUserDbContext userDbContext, IGroupDbContext groupDbContext, IShortnameDbContext shortnameDbContext, IMapper mapper)
    {
        _userDbContext = userDbContext;
        _groupDbContext = groupDbContext;
        _shortnameDbContext = shortnameDbContext;
        _mapper = mapper;
    }
    
    public async Task<GroupDetailsVm> Handle(GetGroupDetailsQuery request, CancellationToken cancellationToken)
    {
        var requester = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.RequesterId, cancellationToken);
        if (requester == null || requester.DeletedAt != null) throw new Exception("User not found");
        
        var group = await _groupDbContext.Groups.FirstOrDefaultAsync(g => g.Id == request.GroupId, cancellationToken);
        if (group == null || group.DeletedAt != null) throw new Exception("Group not found");
        
        var shortname = await _shortnameDbContext.Shortnames.FirstAsync(s => s.Owner == ShortnameOwner.Group && s.OwnerId == group.Id, cancellationToken)!;
        
        var vm = _mapper.Map<GroupDetailsVm>(group);
        vm.Shortname = shortname.Shortname;
        
        return vm;
    }
}