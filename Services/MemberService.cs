using Application.Interfaces;
using Application.Interfaces.Contexts;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class MemberService : IMembersService
{
    private readonly IMemberDbContext _context;

    public MemberService(IMemberDbContext context)
    {
        _context = context;
    }
    
    public async Task<Member?> GetMemberAsync(Guid memberId, CancellationToken cancellationToken)
    {
        var member = await _context.Members.FirstOrDefaultAsync(m => m.Id == memberId && m.BannedAt == null, cancellationToken);
        return member;
    }

    public async Task<Member?> GetMemberAsync(Guid userId, Guid groupId, CancellationToken cancellationToken)
    {
        var member = await _context.Members.FirstOrDefaultAsync(m => m.Id == userId && m.GroupId == groupId && m.BannedAt == null, cancellationToken);
        return member;
    }

    public async Task<IEnumerable<Member>> GetMembersAsync(Guid groupId, int offset, int limit, CancellationToken cancellationToken)
    {
        var members = await _context.Members
            .Where(m => m.GroupId == groupId)
            .Skip(offset)
            .Take(limit)
            .ToListAsync(cancellationToken);
        
        return members;
    }

    public async Task<Member> CreateMemberAsync(Guid userId, Guid groupId, CancellationToken cancellationToken)
    {
        var member = new Member
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            GroupId = groupId,
            Role = GroupRole.Member,
            Permissions = GroupMemberPermission.CanWriteMessage | GroupMemberPermission.CanEditOwnMessage | GroupMemberPermission.CanDeleteOwnMessage,
        };
        
         await _context.Members.AddAsync(member, cancellationToken);
         await _context.SaveChangesAsync(cancellationToken);
         
         return member;
    }

    public async Task<Member> CreateAdminAsync(Guid userId, Guid groupId, CancellationToken cancellationToken)
    {
        var member = new Member
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            GroupId = groupId,
            Role = GroupRole.Admin,
            Permissions = GroupMemberPermission.CanWriteMessage | GroupMemberPermission.CanEditOwnMessage | GroupMemberPermission.CanDeleteOwnMessage |
                          GroupMemberPermission.CanDeleteAnyMessage | GroupMemberPermission.CanInviteMembers | GroupMemberPermission.CanMuteMembers |
                          GroupMemberPermission.CanBanMembers | GroupMemberPermission.CanPinMessages,
        };
        
        await _context.Members.AddAsync(member, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
         
        return member;
    }

    public async Task<Member> CreateOwnerAsync(Guid userId, Guid groupId, CancellationToken cancellationToken)
    {
        var member = new Member
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            GroupId = groupId,
            Role = GroupRole.Owner,
            Permissions = GroupMemberPermission.CanWriteMessage | GroupMemberPermission.CanEditOwnMessage | GroupMemberPermission.CanDeleteOwnMessage |
                          GroupMemberPermission.CanDeleteAnyMessage | GroupMemberPermission.CanInviteMembers | GroupMemberPermission.CanMuteMembers |
                          GroupMemberPermission.CanBanMembers | GroupMemberPermission.CanPinMessages | GroupMemberPermission.CanManageRoles |
                          GroupMemberPermission.CanManageGroup | GroupMemberPermission.CanDeleteGroup,
        };
        
        await _context.Members.AddAsync(member, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
         
        return member;
    }

    public async Task UpdateMemberAsync(Guid memberId, GroupRole role, GroupMemberPermission permission, string nickname, CancellationToken cancellationToken)
    {
        var member = await GetMemberAsync(memberId, cancellationToken);
        if (member == null) throw new Exception("Member not found");
        
        member.Role = role;
        member.Permissions = permission;
        member.Nickname = nickname;
        
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task MuteMemberAsync(Guid memberId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task BanMemberAsync(Guid memberId, CancellationToken cancellationToken)
    {
        var member = await GetMemberAsync(memberId, cancellationToken);
        if (member == null) throw new Exception("Member not found");
        
        member.BannedAt = DateTimeOffset.UtcNow;
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}