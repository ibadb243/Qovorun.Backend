using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Group.Commands.UpdateCommand;

public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand>
{
    private readonly IUserDbContext _userDbContext;
    private readonly IGroupDbContext _groupDbContext;
    private readonly IMemberDbContext _memberDbContext;
    private readonly IPermissionDbContext _permissionDbContext;
    
    public UpdateGroupCommandHandler(IUserDbContext userDbContext, IGroupDbContext groupDbContext, IMemberDbContext memberDbContext, IPermissionDbContext permissionDbContext)
    {
        _userDbContext = userDbContext;
        _groupDbContext = groupDbContext;
        _memberDbContext = memberDbContext;
        _permissionDbContext = permissionDbContext;
    }
    
    public async Task Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var requester = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.RequesterId, cancellationToken);
        if (requester == null || requester.DeletedAt != null) throw new Exception("User not found");
        
        var group = await _groupDbContext.Groups.FirstOrDefaultAsync(g => g.Id == request.GroupId, cancellationToken);
        if (group == null  || group.DeletedAt != null) throw new Exception("Group not found");
        
        var member = await _memberDbContext.Members.FirstOrDefaultAsync(m => m.UserId == requester.Id && m.GroupId == group.Id, cancellationToken);
        if (member == null || member.BannedAt != null) throw new Exception("You are not a member of this group");

        var permission = await _permissionDbContext.Permissions.FirstAsync(p => p.Id == member.PermissionId, cancellationToken)!;
        if (permission.CanEditGroupInformation is false) throw new Exception("You do not have permission to edit this group");
        
        group.Name = request.Name;
        group.Description = request.Description;
        group.UpdatedAt = DateTimeOffset.UtcNow;
        
        await _groupDbContext.SaveChangesAsync(cancellationToken);
    }
}