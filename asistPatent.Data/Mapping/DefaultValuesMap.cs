using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using asistPatentCore.Model;
namespace asistPatentCore.Data.Mapping
{
    public class DefaultValuesMap : IEntityTypeConfiguration<DefaultValues>
    {
        public void Configure(EntityTypeBuilder<DefaultValues> builder)
        {
           
            builder.HasKey(u => u.id);
            builder.Property(u => u.key).IsRequired().HasMaxLength(100);
            builder.Property(u => u.value).IsRequired();
            builder.Property(u =>u.status).IsRequired();

        }
    }
}
