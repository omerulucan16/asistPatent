using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using asistPatentCore.Model;
namespace asistPatentCore.Data.Mapping
{
    public class ApplicationSubClassMap : IEntityTypeConfiguration<ApplicationSubClass>
    {
        public void Configure(EntityTypeBuilder<ApplicationSubClass> builder)
        {
            // Override nvarchar(max) with nvarchar(15)
            builder.HasKey(u => u.id);
            builder.Property(u => u.CreateDate).IsRequired();
            builder.Property(u => u.status).IsRequired();
            builder.Property(u => u.subclassname).IsRequired();
            builder.Property(u => u.subclasscode).IsRequired().HasMaxLength(25);
            builder.HasOne<ApplicationClass>(e => e.ApplicationClass)
           .WithMany(d => d.ApplicationSubClassesList)
           .HasForeignKey(e => e.appclassId);
        }

    }
}
