using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using asistPatentCore.Model;
namespace asistPatentCore.Data.Mapping
{
    public class BrandApplicationTypesMap : IEntityTypeConfiguration<BrandApplicationTypes>
    {
        public void Configure(EntityTypeBuilder<BrandApplicationTypes> builder)
        {
            // Override nvarchar(max) with nvarchar(15)
            builder.HasKey(u => u.id);
            builder.Property(u => u.applicationType).IsRequired();
            builder.Property(u => u.categoryType).IsRequired();
            builder.Property(u => u.code).IsRequired().HasMaxLength(10);
            builder.Property(u => u.name).IsRequired();
            builder.Property(u => u.userCreateDate).IsRequired().HasMaxLength(255);
        }
    }
}
