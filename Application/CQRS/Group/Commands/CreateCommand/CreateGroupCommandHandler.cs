using Application.Interfaces;
using Domain;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.User.Commands.CreateCommand;

public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Guid>
{
    private readonly IUserDbContext _userDbContext;
    private readonly IGroupDbContext _groupDbContext;
    private readonly IMemberDbContext _memberDbContext;
    private readonly IShortnameDbContext _shortnameDbContext;
    private readonly IPermissionDbContext _permissionDbContext;
    
    public CreateGroupCommandHandler(
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

    public async Task<Guid> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var requester = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.RequesterId, cancellationToken);
        if (requester == null || requester.DeletedAt != null) throw new Exception("User not found");

        var shortname = await _shortnameDbContext.Shortnames.FirstOrDefaultAsync(s => s.Shortname == request.Shortname, cancellationToken);
        if (shortname != null && shortname.Owner != ShortnameOwner.None) throw new Exception("the short name is already taken");
        
        var group = new Domain.Entities.Group()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            CreatedAt = DateTimeOffset.UtcNow,
        };
        
        if (shortname == null)
        {
            shortname = new ShortnameField
            {
                Id = Guid.NewGuid(),
                Owner = ShortnameOwner.Group,
                OwnerId = group.Id,
                Shortname = request.Shortname,
                CreatedAt = DateTimeOffset.UtcNow,
            };
            
            await _shortnameDbContext.Shortnames.AddAsync(shortname, cancellationToken);
        }
        else
        {
            shortname.Owner = ShortnameOwner.Group;
            shortname.OwnerId = group.Id;
            shortname.ChangedOwnerAt = requester.CreatedAt;
        }

        var permission = new Permission
        {
            Id = Guid.NewGuid(),
            CanDeleteGroup = true,
            CanEditGroupInformation = true,
            CanExcludeMember = true,
            CanGivePermissions = true,
            CanInviteNewMembers = true,
            CanWriteMessage = true,
        };

        var member = new Domain.Entities.Member
        {
            Id = Guid.NewGuid(),
            UserId = requester.Id,
            GroupId = group.Id,
            PermissionId = permission.Id,
            Nickname = "Owner",
            CreatedAt = DateTimeOffset.UtcNow,
        };
        
        await _groupDbContext.Groups.AddAsync(group, cancellationToken);
        await _groupDbContext.SaveChangesAsync(cancellationToken);

        await _shortnameDbContext.SaveChangesAsync(cancellationToken);
        
        await _permissionDbContext.Permissions.AddAsync(permission, cancellationToken);
        await _permissionDbContext.SaveChangesAsync(cancellationToken);
        
        await _memberDbContext.Members.AddAsync(member, cancellationToken);
        await _memberDbContext.SaveChangesAsync(cancellationToken);
        
        return group.Id;
    }
}