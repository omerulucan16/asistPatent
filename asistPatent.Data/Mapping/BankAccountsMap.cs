using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using asistPatentCore.Model;
namespace asistPatentCore.Data.Mapping
{
    public class BankAccountsMap : IEntityTypeConfiguration<BankAccounts>
    {
        public void Configure(EntityTypeBuilder<BankAccounts> builder)
        {
            // Override nvarchar(max) with nvarchar(15)
            builder.HasKey(u => u.id);
            builder.Property(u => u.bankName).HasMaxLength(255).IsRequired();
            builder.Property(u => u.accountCurrencyType).IsRequired();
            builder.Property(u => u.accountNumber).HasMaxLength(255).IsRequired();
            builder.Property(u => u.brunchCode).HasMaxLength(50).IsRequired();
            builder.Property(u => u.brunchName).HasMaxLength(255).IsRequired();
            builder.Property(u => u.ibanNo).IsRequired();
            builder.Property(u => u.status).IsRequired();

        }
    }
}
