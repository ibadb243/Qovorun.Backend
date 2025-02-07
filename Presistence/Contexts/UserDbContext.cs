using Application.Interfaces;
using Application.Interfaces.Contexts;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Presistence.EntityTypeConfigurations;

namespace Presistence.Contexts;

public class UserDbContext : DbContext, IUserDbContext
{
    public DbSet<User> Users { get; set; }

    public UserDbContext(DbContextOptions<UserDbContext> options) 
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        
        base.OnModelCreating(builder);
    }
}