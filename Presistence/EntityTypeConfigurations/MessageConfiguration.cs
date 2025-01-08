using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.EntityTypeConfigurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(_ => _.Id);
        builder.HasIndex(_ => _.Id).IsUnique();
        
        builder.HasIndex(_ => _.GroupId);
        builder.Property(_ => _.GroupId).IsRequired();
        
        builder.HasIndex(_ => _.UserId);
        builder.Property(_ => _.UserId).IsRequired();
    }
}