using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Member.Commands.DeleteCommand;

public class BanMemberCommandHandler : IRequestHandler<BanMemberCommand>
{
    private readonly IUserDbContext _userDbContext;
    private readonly IGroupDbContext _groupDbContext;
    private readonly IMemberDbContext _memberDbContext;
    private readonly IPermissionDbContext _permissionDbContext;

    public BanMemberCommandHandler(
        IUserDbContext userDbContext, 
        IGroupDbContext groupDbContext, 
        IMemberDbContext memberDbContext, 
        IPermissionDbContext permissionDbContext)
    {
        _userDbContext = userDbContext;
        _groupDbContext = groupDbContext;
        _memberDbContext = memberDbContext;
        _permissionDbContext = permissionDbContext;
    }
    
    public async Task Handle(BanMemberCommand request, CancellationToken cancellationToken)
    {
        var requester = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.RequesterId, cancellationToken);
        if (requester == null || requester.DeletedAt != null) throw new Exception("User not found");
        
        var member = await _memberDbContext.Members.FirstOrDefaultAsync(m => m.Id == request.MemberId, cancellationToken);
        if (member == null || member.BannedAt != null) throw new Exception("Member not found");

        if (requester.Id == member.UserId) return;
        
        var requester_member = await _memberDbContext.Members.FirstOrDefaultAsync(m => m.UserId == requester.Id && m.GroupId == member.GroupId, cancellationToken);
        if (requester_member == null || requester_member.BannedAt != null) throw new Exception("You are not mates");
            
        var permission = await _permissionDbContext.Permissions.FirstAsync(p => p.Id == requester_member.PermissionId, cancellationToken)!;
        if (!permission.CanExcludeMember) throw new Exception("You do not have permission to delete this member");
        
        member.BannedAt = DateTimeOffset.UtcNow;
        
        await _memberDbContext.SaveChangesAsync(cancellationToken);
    }
}