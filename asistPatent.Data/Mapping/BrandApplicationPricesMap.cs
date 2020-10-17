using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using asistPatentCore.Model;
namespace asistPatentCore.Data.Mapping
{
    public class BrandApplicationPricesMap : IEntityTypeConfiguration<BrandApplicationPrices>
    {
        public void Configure(EntityTypeBuilder<BrandApplicationPrices> builder)
        {
            // Override nvarchar(max) with nvarchar(15)
            builder.HasKey(u => u.id);
            builder.Property(u => u.service).IsRequired();
            builder.Property(u => u.tuition).IsRequired();
            builder.Property(u => u.branch).IsRequired();
            builder.HasOne<BrandApplicationTypes>(e => e.BrandApplicationTypes)
           .WithMany(d => d.BrandApplicationPricesRelation)
           .HasForeignKey(e => e.brandAppTypeId);
        }
    }
}
