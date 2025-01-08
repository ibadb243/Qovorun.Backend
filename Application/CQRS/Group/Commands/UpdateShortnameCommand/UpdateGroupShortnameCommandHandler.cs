using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Group.Commands.UpdateShortnameCommand;

public class UpdateGroupShortnameCommandHandler : IRequestHandler<UpdateGroupShortnameCommand>
{
    private readonly IUserDbContext _userDbContext;
    private readonly IGroupDbContext _groupDbContext;
    private readonly IMemberDbContext _memberDbContext;
    private readonly IShortnameDbContext _shortnameDbContext;
    private readonly IPermissionDbContext _permissionDbContext;

    public UpdateGroupShortnameCommandHandler(
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
    
    public async Task Handle(UpdateGroupShortnameCommand request, CancellationToken cancellationToken)
    {
        var requester = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.RequesterId, cancellationToken);
        if (requester == null || requester.DeletedAt != null) throw new Exception("User not found");
        
        var group = await _groupDbContext.Groups.FirstOrDefaultAsync(g => g.Id == request.GroupId, cancellationToken);
        if (group == null  || group.DeletedAt != null) throw new Exception("Group not found");
        
        var member = await _memberDbContext.Members.FirstOrDefaultAsync(m => m.UserId == requester.Id && m.GroupId == group.Id, cancellationToken);
        if (member == null || member.BannedAt != null) throw new Exception("You are not a member of this group");

        var permission = await _permissionDbContext.Permissions.FirstAsync(p => p.Id == member.PermissionId, cancellationToken)!;
        if (permission.CanEditGroupInformation is false) throw new Exception("You do not have permission to edit this group");
        
        var shortname = await _shortnameDbContext.Shortnames.FirstAsync(s => s.Owner == ShortnameOwner.Group && s.OwnerId == group.Id, cancellationToken)!;
        if (shortname.Shortname == request.Shortname) return;
        
        var new_shortname = await _shortnameDbContext.Shortnames.FirstOrDefaultAsync(s => s.Shortname == request.Shortname, cancellationToken);
        if (new_shortname != null && new_shortname.Owner != ShortnameOwner.None) throw new Exception("The short name is already taken");
        
        if (new_shortname == null)
        {
            new_shortname = new ShortnameField
            {
                Id = Guid.NewGuid(),
                Owner = ShortnameOwner.Group,
                OwnerId = requester.Id,
                Shortname = request.Shortname,
                CreatedAt = DateTimeOffset.UtcNow,
            };
            
            await _shortnameDbContext.Shortnames.AddAsync(new_shortname, cancellationToken);
        }
        else
        {
            new_shortname.Owner = ShortnameOwner.Group;
            new_shortname.OwnerId = requester.Id;
            new_shortname.ChangedOwnerAt = DateTimeOffset.UtcNow;
        }

        shortname.Owner = ShortnameOwner.None;
        shortname.ChangedOwnerAt = DateTimeOffset.UtcNow;

        await _shortnameDbContext.SaveChangesAsync(cancellationToken);
    }
}