using Application.Interfaces;
using Application.Interfaces.Contexts;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class GroupService : IGroupService
{
    private readonly IGroupDbContext _context;

    public GroupService(IGroupDbContext context)
    {
        _context = context;
    }
    
    public async Task<Group?> GetGroupAsync(Guid groupId, CancellationToken cancellationToken)
    {
        var group = await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId && g.DeletedAt == null, cancellationToken);
        return group;
    }

    public async Task<Group> CreateGroupAsync(string name, string description, CancellationToken cancellationToken)
    {
        var group = new Group
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            CreatedAt = DateTimeOffset.UtcNow,
        };
        
        await _context.Groups.AddAsync(group, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return group;
    }

    public async Task UpdateGroupAsync(Guid groupId, string name, string description, CancellationToken cancellationToken)
    {
        var group = await GetGroupAsync(groupId, cancellationToken);
        if (group == null) throw new Exception("Group not found");
        
        group.Name = name;
        group.Description = description;
        group.UpdatedAt = DateTimeOffset.UtcNow;
        
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteGroupAsync(Guid groupId, CancellationToken cancellationToken)
    {
        var group = await GetGroupAsync(groupId, cancellationToken);
        if (group == null) throw new Exception("Group not found");
        
        group.DeletedAt = DateTimeOffset.UtcNow;
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}