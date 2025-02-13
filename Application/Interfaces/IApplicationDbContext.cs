using Domain.Entities;
using Domain.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Conference> Conferences { get; set; }
    DbSet<Chat> Chats { get; set; }
    DbSet<Group> Groups { get; set; }
    DbSet<Channel> Channels { get; set; }
    DbSet<ShortnameField> Shortnames { get; set; }
    DbSet<Member> Members { get; set; }
    DbSet<Message> Messages { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}