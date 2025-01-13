using Domain.Entities;

namespace Application.Interfaces.Services;

public interface IGroupService
{
    public Task<Group?> GetGroupAsync(Guid groupId, CancellationToken cancellationToken);
    public Task<Group> CreateGroupAsync(string name, string description, CancellationToken cancellationToken);
    public Task UpdateGroupAsync(Guid groupId, string name, string description, CancellationToken cancellationToken);
    public Task DeleteGroupAsync(Guid groupId, CancellationToken cancellationToken);
}