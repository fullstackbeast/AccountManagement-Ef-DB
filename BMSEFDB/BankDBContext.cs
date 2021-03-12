//using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore;

namespace BMSEFDB
{
    class BankDBContext : DbContext
    {
        public BankDBContext( DbContextOptions<BankDBContext> options) : base(options)
        {
            
        }

        public static BankDBContext Connect(string connectionString)
        {
            var optionBuilder = new DbContextOptionsBuilder<BankDBContext>();
            optionBuilder.UseMySQL(connectionString);
            var context = new BankDBContext(optionBuilder.Options);
            context.Database.EnsureCreated();
            return context;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountHolder>().HasOne(ach => ach.Account).WithOne(ac => ac.AccountHolder);
            modelBuilder.Entity<Account>().HasKey(ac => ac.AccountHolderId);    
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountHolder> AccountHolders { get; set; }

        public DbSet<Manager> Managers { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Overdraft> Overdrafts { get; set; }
    }
}
