using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Contexts;

public class MemberDbContext : DbContext, IMemberDbContext
{
    public DbSet<Member> Members { get; set; }
}