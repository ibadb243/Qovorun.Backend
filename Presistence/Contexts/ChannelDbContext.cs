using Application.Interfaces;
using Application.Interfaces.Contexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Contexts;

public class ChannelDbContext : DbContext, IChannelDbContext
{
    public DbSet<Channel> Channels { get; set; }

    public ChannelDbContext(DbContextOptions<ChannelDbContext> options)
        : base(options) { }
}