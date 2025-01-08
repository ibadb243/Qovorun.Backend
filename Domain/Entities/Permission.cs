namespace Domain.Entities;

public class Permission
{
    public Guid Id { get; set; }
    
    public bool CanWriteMessage { get; set; }
    public bool CanInviteNewMembers { get; set; }
    public bool CanEditGroupInformation { get; set; }
    public bool CanExcludeMember { get; set; }
    public bool CanGivePermissions { get; set; }
    public bool CanDeleteGroup { get; set; }
    
    public DateTimeOffset? UpdatedAt { get; set; }
}