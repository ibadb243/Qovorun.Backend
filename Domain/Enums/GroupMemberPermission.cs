namespace Domain.Enums;

[Flags]
public enum GroupMemberPermission
{
    None = 0,
    CanWriteMessage = 1 << 0,
    CanEditOwnMessage = 1 << 1,
    CanDeleteOwnMessage = 1 << 2,
    CanEditAnyMessage = 1 << 3,
    CanDeleteAnyMessage = 1 << 4,
    CanInviteMembers = 1 << 5,
    CanMuteMembers = 1 << 6,
    CanBanMembers = 1 << 7,
    CanManageRoles = 1 << 8,
    CanPinMessages = 1 << 9,
    CanManageGroup = 1 << 10,
    CanDeleteGroup = 1 << 11,
}