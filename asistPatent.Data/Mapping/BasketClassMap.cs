using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using asistPatentCore.Model;
namespace asistPatentCore.Data.Mapping
{
    public class BasketClassMap : IEntityTypeConfiguration<BasketClass>
    {
        public void Configure(EntityTypeBuilder<BasketClass> builder)
        {
            // Override nvarchar(max) with nvarchar(15)
            builder.HasKey(u => u.Id);
            builder.Property(u => u.status).IsRequired();
            builder.Property(u => u.basketClassName).IsRequired();
            builder.Property(u => u.createDate).IsRequired();
            builder.HasOne<Basket>(e => e.Basket)
           .WithMany(d => d.BasketClassBasketRelation)
           .HasForeignKey(e => e.basketId);
            builder.HasOne(e => e.ApplicationClass)
           .WithMany(d => d.BasketClassesList)
           .HasForeignKey(e => e.appClassId).IsRequired();


        }
    }
}
