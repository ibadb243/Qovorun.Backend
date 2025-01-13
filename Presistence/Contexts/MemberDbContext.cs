using Application.Interfaces;
using Application.Interfaces.Contexts;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Contexts;

public class MemberDbContext : DbContext, IMemberDbContext
{
    public DbSet<Member> Members { get; set; }

    public MemberDbContext(DbContextOptions<MemberDbContext> options)
        : base(options) { }
}