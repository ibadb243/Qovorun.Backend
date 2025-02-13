using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Presistence.EntityTypeConfigurations;

namespace Presistence.Contexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Conference> Conferences { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Channel> Channels { get; set; }
    public DbSet<ShortnameField> Shortnames { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Message> Messages { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        
        builder.ApplyConfiguration(new ConferenceConfiguration());
        builder.ApplyConfiguration(new ChatConfiguration());
        builder.ApplyConfiguration(new GroupConfiguration());
        builder.ApplyConfiguration(new ChannelConfiguration());
        
        builder.ApplyConfiguration(new ShortnameConfiguration());
        
        builder.ApplyConfiguration(new MemberConfiguration());
        
        builder.ApplyConfiguration(new MessageConfiguration());
        
        base.OnModelCreating(builder);
    }
}