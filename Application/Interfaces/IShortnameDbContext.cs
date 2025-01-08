using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IShortnameDbContext
{
    public DbSet<ShortnameField> Shortnames { get; set; }
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}