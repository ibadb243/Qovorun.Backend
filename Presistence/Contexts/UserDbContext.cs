using Application.Interfaces;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Contexts;

public class UserDbContext : DbContext, IUserDbContext
{
    public DbSet<User> Users { get; set; }

    public UserDbContext(DbContextOptions<UserDbContext> options) 
        : base(options) { }
}