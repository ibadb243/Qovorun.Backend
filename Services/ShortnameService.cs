using Application.Interfaces;
using Application.Interfaces.Contexts;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class ShortnameService : IShortnameService
{
    private readonly IShortnameDbContext _context;

    public ShortnameService(IShortnameDbContext context)
    {
        _context = context;
    }

    public async Task<ShortnameField?> GetShortnameAsync(ShortnameOwner owner, Guid ownerId, CancellationToken cancellationToken)
    {
        var shortnameF = await _context.Shortnames.FirstOrDefaultAsync(s => s.Owner == owner && s.OwnerId == ownerId, cancellationToken);
        return shortnameF;
    }

    public async Task<ShortnameField?> GetShortnameAsync(string shortname, CancellationToken cancellationToken)
    {
        var shortnameF = await _context.Shortnames.FirstOrDefaultAsync(s => s.Shortname == shortname, cancellationToken);
        return shortnameF;
    }

    public async Task<ShortnameField> CreateShortnameAsync(string shortname, ShortnameOwner owner, Guid ownerId, CancellationToken cancellationToken)
    {
        var shortnameF = await GetShortnameAsync(shortname, cancellationToken);
        if (shortnameF != null) throw new Exception("Shortname already exists");

        shortnameF = new ShortnameField
        {
            Id = Guid.NewGuid(),
            Shortname = shortname,
            Owner = owner,
            OwnerId = ownerId,
            CreatedAt = DateTimeOffset.UtcNow
        };
        
        await _context.Shortnames.AddAsync(shortnameF, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return shortnameF;
    }

    public async Task UpdateShortnameAsync(string shortname, ShortnameOwner owner, Guid ownerId, CancellationToken cancellationToken)
    {
        var shortnameF = await GetShortnameAsync(shortname, cancellationToken);
        if (shortnameF == null) throw new Exception("Shortname doesn't exist");
        
        if (shortnameF.Owner != ShortnameOwner.None) throw new Exception("Shortname already owned");
        
        shortnameF.Owner = owner;
        shortnameF.OwnerId = ownerId;
        shortnameF.ChangedOwnerAt = DateTimeOffset.UtcNow;
        
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsShortnameFreeAsync(string shortname, CancellationToken cancellationToken)
    {
        var shortnameF = await _context.Shortnames.FirstOrDefaultAsync(s => s.Shortname == shortname, cancellationToken);
        return shortnameF == null || shortnameF.Owner == ShortnameOwner.None;
    }

    public async Task ClearShortnameAsync(ShortnameOwner owner, Guid ownerId, CancellationToken cancellationToken)
    {
        var shortnameF = await _context.Shortnames.FirstOrDefaultAsync(s => s.Owner == owner && s.OwnerId == ownerId, cancellationToken);
        if (shortnameF == null) throw new Exception("Shortname doesn't exist");
        
        shortnameF.Owner = ShortnameOwner.None;
        shortnameF.OwnerId = Guid.Empty;
        shortnameF.ChangedOwnerAt = DateTimeOffset.UtcNow;
        
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task ClearShortnameAsync(string shortname, CancellationToken cancellationToken)
    {
        var shortnameF = await GetShortnameAsync(shortname, cancellationToken);
        if (shortnameF == null) throw new Exception("Shortname doesn't exist");
        
        shortnameF.Owner = ShortnameOwner.None;
        shortnameF.OwnerId = Guid.Empty;
        shortnameF.ChangedOwnerAt = DateTimeOffset.UtcNow;
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}