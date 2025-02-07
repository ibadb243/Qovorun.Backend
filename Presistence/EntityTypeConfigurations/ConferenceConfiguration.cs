using Domain.Entities;
using Domain.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.EntityTypeConfigurations;

public class ConferenceConfiguration : IEntityTypeConfiguration<Conference>
{
    public void Configure(EntityTypeBuilder<Conference> builder)
    {
        builder.HasDiscriminator<string>("ConferenceType")
            .HasValue<Chat>("Chat")
            .HasValue<Group>("Group")
            .HasValue<Channel>("Channel");
        
        builder.HasKey(_ => _.Id);
        builder.HasIndex(_ => _.Id).IsUnique();
    }
}