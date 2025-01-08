using Application.Interfaces;
using Domain;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Member.Commands.CreateCommand;

public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Guid>
{
    private readonly IUserDbContext _userDbContext;
    private readonly IGroupDbContext _groupDbContext;
    private readonly IMemberDbContext _memberDbContext;
    private readonly IPermissionDbContext _permissionDbContext;

    public CreateMemberCommandHandler(IUserDbContext userDbContext, IGroupDbContext groupDbContext, IMemberDbContext memberDbContext, IPermissionDbContext permissionDbContext)
    {
        _userDbContext = userDbContext;
        _groupDbContext = groupDbContext;
        _memberDbContext = memberDbContext;
        _permissionDbContext = permissionDbContext;
    }
    
    public async Task<Guid> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var requester = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.RequesterId, cancellationToken);
        if (requester == null || requester.DeletedAt != null) throw new Exception("User not found");
        
        var user = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        if (user == null || user.DeletedAt != null) throw new Exception("User not found");
        
        var group = await _groupDbContext.Groups.FirstOrDefaultAsync(g => g.Id == request.GroupId, cancellationToken);
        if (group == null || group.DeletedAt != null) throw new Exception("Group not found");

        var permission = new Permission
        {
            Id = Guid.NewGuid(),
            CanWriteMessage = true,
            CanInviteNewMembers = true,
        };

        var member = new Domain.Entities.Member
        {
            Id = Guid.NewGuid(),
            UserId = requester.Id,
            GroupId = group.Id,
            PermissionId = permission.Id,
            Nickname = string.Empty,
            CreatedAt = DateTimeOffset.UtcNow,
        };
        
        await _permissionDbContext.Permissions.AddAsync(permission, cancellationToken);
        await _permissionDbContext.SaveChangesAsync(cancellationToken);
        
        await _memberDbContext.Members.AddAsync(member, cancellationToken);
        await _memberDbContext.SaveChangesAsync(cancellationToken);
        
        return member.Id;
    }
}