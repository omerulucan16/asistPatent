using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using asistPatentCore.Model;
namespace asistPatentCore.Data.Mapping
{
    public class ApplicationClassMap : IEntityTypeConfiguration<ApplicationClass>
    {
        public void Configure(EntityTypeBuilder<ApplicationClass> builder)
        {
            // Override nvarchar(max) with nvarchar(15)
            builder.HasKey(u => u.id);
            builder.Property(u => u.CreateDate).IsRequired();
            builder.Property(u => u.appclassnumber).IsRequired();
            builder.Property(u => u.appclassname).IsRequired().HasMaxLength(255);
            
        }

    }
}
