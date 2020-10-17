using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using asistPatentCore.Model;
namespace asistPatentCore.Data.Mapping
{
    public class AgreementsRolesMap : IEntityTypeConfiguration<AgreementsRoles>
    {
        public void Configure(EntityTypeBuilder<AgreementsRoles> builder)
        {
            // Override nvarchar(max) with nvarchar(15)
            builder.HasKey(u => u.id);

            builder.HasOne<Users>(e => e.Users)
         .WithMany(d => d.UsersAgreementsRolesRelation)
         .HasForeignKey(e => e.userId);

            builder.HasOne<AgreementStatuses>(e => e.AgreementStatuses)
         .WithMany(d => d.AgreementsRolesRelation)
         .HasForeignKey(e => e.agreementsStatusesId);


        }
    }
}
