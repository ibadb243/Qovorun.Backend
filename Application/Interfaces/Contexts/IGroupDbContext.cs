using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Contexts;

public interface IGroupDbContext
{
    public DbSet<Group> Groups { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}