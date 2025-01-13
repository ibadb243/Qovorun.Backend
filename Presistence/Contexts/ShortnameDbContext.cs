using Application.Interfaces;
using Application.Interfaces.Contexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Contexts;

public class ShortnameDbContext : DbContext, IShortnameDbContext
{
    public DbSet<ShortnameField> Shortnames { get; set; }

    public ShortnameDbContext(DbContextOptions<ShortnameDbContext> options)
        : base(options) { }
}