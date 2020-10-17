using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using asistPatentCore.Model;
namespace asistPatentCore.Data.Mapping
{
    public class ApplicationEmtiaClassMap : IEntityTypeConfiguration<ApplicationEmtiaClass>
    {
        public void Configure(EntityTypeBuilder<ApplicationEmtiaClass> builder)
        {
            // Override nvarchar(max) with nvarchar(15)
            builder.HasKey(u => u.id);
            builder.Property(u => u.CreateDate).IsRequired();
            builder.Property(u => u.status).IsRequired();
            builder.Property(u => u.value).IsRequired();
            builder.HasOne<ApplicationSubClass>(e => e.ApplicationSubClass)
           .WithMany(d => d.ApplicationEmtiaClassesList)
           .HasForeignKey(e => e.appSubClassId);
        }

    }
}
