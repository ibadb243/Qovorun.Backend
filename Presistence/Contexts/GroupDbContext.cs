using Application.Interfaces;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Contexts;

public class GroupDbContext : DbContext, IGroupDbContext
{
    public DbSet<Group> Groups { get; set; }

    public GroupDbContext(DbContextOptions<GroupDbContext> options)
        : base(options) { }
}