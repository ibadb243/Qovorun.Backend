using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IChannelDbContext
{
    public DbSet<Channel> Channels { get; set; }
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}