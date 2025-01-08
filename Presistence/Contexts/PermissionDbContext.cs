using Application.Interfaces;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Contexts;

public class PermissionDbContext : DbContext, IPermissionDbContext
{
    public DbSet<Permission> Permissions { get; set; }

    public PermissionDbContext(DbContextOptions<UserDbContext> options) 
        : base(options) { }
}