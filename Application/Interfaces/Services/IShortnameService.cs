using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces.Services;

public interface IShortnameService
{
    public Task<ShortnameField?> GetShortnameAsync(ShortnameOwner owner, Guid ownerId, CancellationToken cancellationToken);
    public Task<ShortnameField?> GetShortnameAsync(string shortname, CancellationToken cancellationToken);
    public Task<ShortnameField> CreateShortnameAsync(string shortname, ShortnameOwner owner, Guid ownerId, CancellationToken cancellationToken);
    public Task UpdateShortnameAsync(string shortname, ShortnameOwner owner, Guid ownerId, CancellationToken cancellationToken);
    public Task<bool> IsShortnameFreeAsync(string shortname, CancellationToken cancellationToken);
    public Task ClearShortnameAsync(ShortnameOwner owner, Guid ownerId, CancellationToken cancellationToken);
    public Task ClearShortnameAsync(string shortname, CancellationToken cancellationToken);
}