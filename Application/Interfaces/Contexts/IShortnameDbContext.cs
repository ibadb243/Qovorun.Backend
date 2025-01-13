using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Contexts;

public interface IShortnameDbContext
{
    public DbSet<ShortnameField> Shortnames { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}