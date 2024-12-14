using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IUserDbContext
{
    public DbSet<User> Users { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}