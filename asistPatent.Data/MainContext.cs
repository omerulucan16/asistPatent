using System;
using asistPatentCore.Model;
using Microsoft.EntityFrameworkCore;
namespace asistPatentCore.Data
{
    public class MainContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(@"server=localhost;database=AsistPatentDev;uid=root;password=;port=3306");//dev
           // optionsBuilder.UseMySql(@"server=localhost;database=u7669216_dbAsist;uid=u7669216_user760;password=IVtc15G0KVvp90B;port=3306");//test
            //optionsBuilder.UseMySql(@"server=localhost;database=AsistPaten;uid=onlineas;password=r6.x:HQy54W7dA;port=3306");
        }
        public DbSet<Users> users { get; set; }
        public DbSet<DefaultValues> defaultValues { get; set; }
        public DbSet<MailSources> mailSources { get; set; }
        public DbSet<MailTemplates> mailTemplates { get; set; }
        public DbSet<UsersToken> usersTokens { get; set; }
        public DbSet<AuthorizedUsers> authorizedUsers { get; set; }
        public DbSet<BrandApplicationTypes> brandApplicationTypes { get; set; }
        public DbSet<ApplicationClass> applicationClasses { get; set; }
        public DbSet<BrandApplicationVisibilty> brandApplicationVisiblities { get; set; }
        public DbSet<ApplicationSubClass> applicationSubClasses { get; set; }
        public DbSet<Companies> companies { get; set; }
        public DbSet<Basket> baskets { get; set; }
        public DbSet<BasketCompany> basketCompanies { get; set; }
        public DbSet<BasketSubClass> basketSubClasses { get; set; }
        public DbSet<ApplicationEmtiaClass> applicationEmtiaClasses { get; set; }
        public DbSet<BasketFiles>  basketFiles { get; set; }
        public DbSet<BrandApplicationPrices> brandApplicationPrices { get; set; }
        public DbSet<BasketClass> basketClass { get; set; }
        public DbSet<BankAccounts> bankAccounts { get; set; }
        public DbSet<AgreementCounter> agreementCounters { get; set; }
        public DbSet<Agreements> agreements { get; set; }
        public DbSet<AgreementsChanges> agreementsChanges { get; set; }
        public DbSet<AgreementStatuses> agreementStatuses { get; set; }
        public DbSet<AgreementsRoles> AgreementsRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Mapping.UserMap());
            modelBuilder.ApplyConfiguration(new Mapping.DefaultValuesMap());
            modelBuilder.ApplyConfiguration(new Mapping.MailSourcesMap());
            modelBuilder.ApplyConfiguration(new Mapping.MailTemplatesMap());
            modelBuilder.ApplyConfiguration(new Mapping.UsersTokenMap());
            modelBuilder.ApplyConfiguration(new Mapping.AuthorizedUsersMap());
            modelBuilder.ApplyConfiguration(new Mapping.BrandApplicationTypesMap());
            modelBuilder.ApplyConfiguration(new Mapping.ApplicationClassMap());
            modelBuilder.ApplyConfiguration(new Mapping.BrandApplicationVisibilityMap());
            modelBuilder.ApplyConfiguration(new Mapping.CompaniesMap());
            modelBuilder.ApplyConfiguration(new Mapping.ApplicationSubClassMap());
            modelBuilder.ApplyConfiguration(new Mapping.BasketMap());
            modelBuilder.ApplyConfiguration(new Mapping.BasketCompanyMap());
            modelBuilder.ApplyConfiguration(new Mapping.BasketSubClassMap());
            modelBuilder.ApplyConfiguration(new Mapping.ApplicationEmtiaClassMap());
            modelBuilder.ApplyConfiguration(new Mapping.BasketFilesMap());
            modelBuilder.ApplyConfiguration(new Mapping.BrandApplicationPricesMap());
            modelBuilder.ApplyConfiguration(new Mapping.BasketClassMap());
            modelBuilder.ApplyConfiguration(new Mapping.BankAccountsMap());
            modelBuilder.ApplyConfiguration(new Mapping.AgreementCounterMap());
            modelBuilder.ApplyConfiguration(new Mapping.AgreementsMap());
            modelBuilder.ApplyConfiguration(new Mapping.AgreementsChangesMap());
            modelBuilder.ApplyConfiguration(new Mapping.AgreementStatusesMap());
            modelBuilder.ApplyConfiguration(new Mapping.AgreementsRolesMap());
        }
    }
}
