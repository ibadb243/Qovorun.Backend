using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Contexts;

public class UserDbContext : DbContext, IUserDbContext
{
    public DbSet<User> Users { get; set; }
}