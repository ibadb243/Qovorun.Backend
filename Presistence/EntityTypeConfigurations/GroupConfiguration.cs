using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.EntityTypeConfigurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(_ => _.Id);
        builder.HasIndex(_ => _.Id).IsUnique();
        
        builder.Property(_ => _.Name).HasMaxLength(128).IsRequired();
        
        builder.HasIndex(_ => _.ShortName).IsUnique();
        builder.Property(_ => _.ShortName).IsRequired().HasMaxLength(64);
        
        builder.Property(_ => _.Description).HasMaxLength(4024);
    }
}