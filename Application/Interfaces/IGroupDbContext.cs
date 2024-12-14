using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IGroupDbContext
{
    public DbSet<Group> Groups { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}