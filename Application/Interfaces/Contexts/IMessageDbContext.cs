using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Contexts;

public interface IMessageDbContext
{
    public DbSet<Message> Messages { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}