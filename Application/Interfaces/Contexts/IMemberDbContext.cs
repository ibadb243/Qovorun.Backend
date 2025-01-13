using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Contexts;

public interface IMemberDbContext
{
    public DbSet<Member> Members { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}