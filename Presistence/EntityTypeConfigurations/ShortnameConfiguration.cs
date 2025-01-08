using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.EntityTypeConfigurations;

public class ShortnameConfiguration : IEntityTypeConfiguration<ShortnameField>
{
    public void Configure(EntityTypeBuilder<ShortnameField> builder)
    {
        builder.HasKey(_ => _.Id);
        builder.HasIndex(_ => _.Id).IsUnique();
        
        builder.HasIndex(_ => _.Shortname).IsUnique();
    }
}