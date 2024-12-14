using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Contexts;

public class GroupDbContext : DbContext, IGroupDbContext
{
    public DbSet<Group> Groups { get; set; }
}