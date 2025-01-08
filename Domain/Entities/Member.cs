namespace Domain.Entities;

public class Member
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
    public Guid PermissionId { get; set; }
    public string Nickname { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? BannedAt { get; set; }
}