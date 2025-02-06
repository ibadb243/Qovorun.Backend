using Domain.Enums;

namespace Domain.Entities;

public class Member
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
    public GroupRole Role { get; set; }
    public GroupMemberPermission Permissions { get; set; }
    public string Nickname { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? BannedAt { get; set; }

    public Member() { }

    public Member(Guid userId, Guid groupId, GroupRole role, GroupMemberPermission permissions, string nickname)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        GroupId = groupId;
        Role = role;
        Permissions = permissions;
        Nickname = nickname;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = null;
        BannedAt = null;
    }
}