using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using asistPatentCore.Model;
namespace asistPatentCore.Data.Mapping
{
    public class MailTemplatesMap : IEntityTypeConfiguration<MailTemplates>
    {
        public void Configure(EntityTypeBuilder<MailTemplates> builder)
        {
            // Override nvarchar(max) with nvarchar(15)
            builder.HasKey(u => u.id);
            builder.Property(u => u.mailContent).IsRequired();
            builder.Property(u => u.mailHeader).IsRequired();
            builder.Property(u => u.template).IsRequired();
        }
    }
}
