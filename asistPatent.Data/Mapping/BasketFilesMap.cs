using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using asistPatentCore.Model;
namespace asistPatentCore.Data.Mapping
{
    public class BasketFilesMap : IEntityTypeConfiguration<BasketFiles>
    {
        public void Configure(EntityTypeBuilder<BasketFiles> builder)
        {
            // Override nvarchar(max) with nvarchar(15)
            builder.HasKey(u => u.Id);
            builder.Property(u => u.status).IsRequired();
            builder.Property(u => u.createDate).IsRequired();
            builder.HasOne<Basket>(e => e.Basket)
           .WithMany(d => d.BasketFilesBasketRelation)
           .HasForeignKey(e => e.basketId);
            


        }
    }
}
