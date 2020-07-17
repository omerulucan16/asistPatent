using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using asistPatentCore.Model;
namespace asistPatentCore.Data.Mapping
{
    public class UsersTokenMap : IEntityTypeConfiguration<UsersToken>
    {
        public void Configure(EntityTypeBuilder<UsersToken> builder)
        {
            // Override nvarchar(max) with nvarchar(15)
            builder.HasKey(u => u.tokenId);
            builder.Property(u => u.createDate).IsRequired();
            builder.Property(u => u.status).IsRequired();
            builder.Property(u => u.userId).IsRequired();
            builder.Property(u => u.type).IsRequired();
            builder.HasOne<Users>(e => e.Users)
            .WithMany(d => d.UsersTokenRelation)
            .HasForeignKey(e => e.userId);
        }

    }
}
