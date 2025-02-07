using Application.Interfaces.Contexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Presistence.EntityTypeConfigurations;

namespace Presistence.Contexts;

public class MemberDbContext : DbContext, IMemberDbContext
{
    public DbSet<Member> Members { get; set; }

    public MemberDbContext(DbContextOptions<MemberDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new MemberConfiguration());
        
        base.OnModelCreating(builder);
    }
}