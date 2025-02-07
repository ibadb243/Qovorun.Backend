using Domain.Entities;
using Domain.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.Contexts;

public interface IConferenceDbContext
{
    public DbSet<Conference> Conferences { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Channel> Channels { get; set; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}