using System;
using asistPatentCore.Model;
using Microsoft.EntityFrameworkCore;
namespace asistPatentCore.Data
{
    public class MainContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseMySql(@"server=localhost;database=AsistPatent;uid=root;password=;port=3306");
            optionsBuilder.UseMySql(@"server=localhost;database=AsistPaten;uid=onlineas;password=r6.x:HQy54W7dA;port=3306");
        }
        public DbSet<Users> users { get; set; }
        public DbSet<DefaultValues> defaultValues { get; set; }
        public DbSet<MailSources> mailSources { get; set; }
        public DbSet<MailTemplates> mailTemplates { get; set; }
        public DbSet<UsersToken> usersTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Mapping.UserMap());
            modelBuilder.ApplyConfiguration(new Mapping.DefaultValuesMap());
            modelBuilder.ApplyConfiguration(new Mapping.MailSourcesMap());
            modelBuilder.ApplyConfiguration(new Mapping.MailTemplatesMap());
            modelBuilder.ApplyConfiguration(new Mapping.UsersTokenMap());
        }
    }
}
