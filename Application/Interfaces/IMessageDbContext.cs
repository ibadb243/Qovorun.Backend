using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IMessageDbContext
{
    public DbSet<Message> Messages { get; set; }
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}