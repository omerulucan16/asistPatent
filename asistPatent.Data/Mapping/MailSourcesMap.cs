using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using asistPatentCore.Model;
namespace asistPatentCore.Data.Mapping
{
    public class MailSourcesMap : IEntityTypeConfiguration<MailSources>
    {
        public void Configure(EntityTypeBuilder<MailSources> builder)
        {
            // Override nvarchar(max) with nvarchar(15)
            builder.HasKey(u => u.id);
            builder.Property(u => u.enableSsl).IsRequired();
            builder.Property(u => u.password).IsRequired();
            builder.Property(u => u.port).IsRequired();
            builder.Property(u => u.smtpServer).IsRequired();
            builder.Property(u => u.username).IsRequired();
        }
    }
}
