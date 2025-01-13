using Application.Interfaces;
using Application.Interfaces.Contexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Contexts;

public class MessageDbContext : DbContext, IMessageDbContext
{
    public DbSet<Message> Messages { get; set; }

    public MessageDbContext(DbContextOptions<MessageDbContext> options)
        : base(options) { }
}