using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.EntityTypeConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(_ => _.Id);
        builder.HasIndex(_ => _.Id).IsUnique();
        
        builder.Property(_ => _.Firstname).IsRequired().HasMaxLength(32);
        
        builder.Property(_ => _.Lastname).HasMaxLength(32);
        
        builder.Property(_ => _.Description).HasMaxLength(1024);
        
        builder.HasIndex(_ => _.PhoneNumber).IsUnique();
        builder.Property(_ => _.PhoneNumber).IsRequired();
        
        builder.Property(_ => _.PasswordHash).IsRequired();
    }
}