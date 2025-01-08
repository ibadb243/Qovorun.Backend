using Application.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Group.Commands.DeleteCommand;

public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand>
{
    private readonly IUserDbContext _userDbContext;
    private readonly IGroupDbContext _groupDbContext;
    private readonly IMemberDbContext _memberDbContext;
    private readonly IShortnameDbContext _shortnameDbContext;
    private readonly IPermissionDbContext _permissionDbContext;
    
    public DeleteGroupCommandHandler(
        IUserDbContext userDbContext, 
        IGroupDbContext groupDbContext, 
        IMemberDbContext memberDbContext, 
        IShortnameDbContext shortnameDbContext,
        IPermissionDbContext permissionDbContext)
    {
        _userDbContext = userDbContext;
        _groupDbContext = groupDbContext;
        _memberDbContext = memberDbContext;
        _shortnameDbContext = shortnameDbContext;
        _permissionDbContext = permissionDbContext;
    }

    public async Task Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var requester = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.RequesterId, cancellationToken);
        if (requester == null || requester.DeletedAt != null) throw new Exception("User not found");
        
        var group = await _groupDbContext.Groups.FirstOrDefaultAsync(g => g.Id == request.GroupId, cancellationToken);
        if (group == null || group.DeletedAt != null) throw new Exception("Group not found");
        
        var member = await _memberDbContext.Members.FirstOrDefaultAsync(m => m.UserId == requester.Id && m.GroupId == group.Id, cancellationToken);
        if (member == null || member.BannedAt != null) throw new Exception("You are not member of this group");
        
        var permission = await _permissionDbContext.Permissions.FirstAsync(p => p.Id == member.PermissionId, cancellationToken)!;
        if (permission.CanDeleteGroup) throw new Exception("You cannot delete this group");
        
        var shortname = await _shortnameDbContext.Shortnames.FirstAsync(s => s.Owner == ShortnameOwner.Group && s.OwnerId == group.Id, cancellationToken)!;
        
        shortname.Owner = ShortnameOwner.None;
        group.DeletedAt = DateTimeOffset.UtcNow;
        
        await _groupDbContext.SaveChangesAsync(cancellationToken);
        await _shortnameDbContext.SaveChangesAsync(cancellationToken);
    }
}