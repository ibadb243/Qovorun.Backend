using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.EntityTypeConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(_ => _.Id);
        builder.HasIndex(_ => _.Id).IsUnique();
        
        builder.Property(_ => _.FirstnName).IsRequired().HasMaxLength(32);
        
        builder.Property(_ => _.LastName).HasMaxLength(32);
        
        builder.HasIndex(_ => _.ShortName).IsUnique();
        builder.Property(_ => _.ShortName).IsRequired().HasMaxLength(64);
        
        builder.Property(_ => _.Description).HasMaxLength(1024);
    }
}