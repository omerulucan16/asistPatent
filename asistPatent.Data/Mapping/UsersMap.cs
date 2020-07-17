using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using asistPatentCore.Model;
namespace asistPatentCore.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            // Override nvarchar(max) with nvarchar(15)
            builder.HasKey(u => u.userId);
            builder.Property(u => u.userId).IsRequired();
            builder.Property(u => u.userName).HasMaxLength(120);
            builder.Property(u => u.userPhoneNumber).HasMaxLength(11);
            builder.Property(u => u.userSurname).HasMaxLength(120);
            
            //builder.Property(u => u.accessToken).HasMaxLength(255);
            
            builder.Property(u => u.status).IsRequired();
            builder.Property(u => u.role).IsRequired();
            builder.Property(u => u.userEmailAdress).IsRequired().HasMaxLength(255);
            builder.Property(u => u.userCreateDate).IsRequired();
            builder.Property(u => u.userPassword).HasMaxLength(24);

        }
    }
}
