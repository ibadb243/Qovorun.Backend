using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IPermissionDbContext
{
    public DbSet<Permission> Permissions { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}