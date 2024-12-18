using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.EntityTypeConfigurations;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasKey(_ => _.Id);
        builder.HasIndex(_ => _.Id).IsUnique();
        
        builder.HasIndex(_ => _.UserId);
        
        builder.HasIndex(_ => _.GroupId);

        builder.HasIndex(_ => _.PermissionId).IsUnique();
        
        builder.Property(_ => _.Nickname).HasMaxLength(32);
    }
}