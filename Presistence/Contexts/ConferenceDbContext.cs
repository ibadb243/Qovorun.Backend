using Application.Interfaces.Contexts;
using Domain.Entities;
using Domain.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Presistence.EntityTypeConfigurations;

namespace Presistence.Contexts;

public class ConferenceDbContext : DbContext, IConferenceDbContext
{
    public DbSet<Conference> Conferences { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Channel> Channels { get; set; }

    public ConferenceDbContext(DbContextOptions<ConferenceDbContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ConferenceConfiguration());
        builder.ApplyConfiguration(new ChatConfiguration());
        builder.ApplyConfiguration(new GroupConfiguration());
        builder.ApplyConfiguration(new ChannelConfiguration());
        
        base.OnModelCreating(builder);
    }
}