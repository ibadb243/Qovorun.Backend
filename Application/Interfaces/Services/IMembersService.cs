using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Services;

public interface IMembersService
{
    public Task<Member?> GetMemberAsync(Guid memberId, CancellationToken cancellationToken);
    public Task<Member?> GetMemberAsync(Guid userId, Guid groupId, CancellationToken cancellationToken);
    public Task<IEnumerable<Member>> GetMembersAsync(Guid groupId, int offset, int limit, CancellationToken cancellationToken);
    public Task<Member> CreateMemberAsync(Guid userId, Guid groupId, CancellationToken cancellationToken);
    public Task<Member> CreateAdminAsync(Guid userId, Guid groupId, CancellationToken cancellationToken);
    public Task<Member> CreateOwnerAsync(Guid userId, Guid groupId, CancellationToken cancellationToken);
    public Task UpdateMemberAsync(Guid memberId, GroupRole role, GroupMemberPermission permission, string nickname, CancellationToken cancellationToken);
    public Task MuteMemberAsync(Guid memberId, CancellationToken cancellationToken);
    public Task BanMemberAsync(Guid memberId, CancellationToken cancellationToken);
}