using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Member.Commands.UpdateCommand;

public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand>
{
    private readonly IUserDbContext _userDbContext;
    private readonly IMemberDbContext _memberDbContext;
    private readonly IPermissionDbContext _permissionDbContext;

    public UpdateMemberCommandHandler(IUserDbContext userDbContext, IMemberDbContext memberDbContext, IPermissionDbContext permissionDbContext)
    {
        _userDbContext = userDbContext;
        _memberDbContext = memberDbContext;
        _permissionDbContext = permissionDbContext;
    }
    
    public async Task Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        var requester = await _userDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.RequesterId, cancellationToken);
        if (requester == null || requester.DeletedAt != null) throw new Exception("User not found");
        
        var member = await _memberDbContext.Members.FirstOrDefaultAsync(m => m.Id == request.MemberId, cancellationToken);
        if (member == null || member.BannedAt != null) throw new Exception("Member not found");

        if (requester.Id == member.UserId) return;
        
        var requester_member = await _memberDbContext.Members.FirstOrDefaultAsync(m => m.UserId == requester.Id && m.GroupId == member.GroupId, cancellationToken);
        if (requester_member == null || requester_member.BannedAt != null) throw new Exception("You are not mates");
        
        member.Nickname = request.Nickname;
        member.UpdatedAt = DateTimeOffset.UtcNow;
        
        await _memberDbContext.SaveChangesAsync(cancellationToken);
    }
}