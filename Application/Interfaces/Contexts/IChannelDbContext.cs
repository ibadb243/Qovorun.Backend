using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Contexts;

public interface IChannelDbContext
{
    public DbSet<Channel> Channels { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}