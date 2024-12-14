using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Contexts;

public class PermissionDbContext : DbContext, IPermissionDbContext
{
    public DbSet<Permission> Permissions { get; set; }
}