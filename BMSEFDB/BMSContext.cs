using System;

using Microsoft.EntityFrameworkCore;

namespace BMSEFDB
{
    public class BMSContext : DbContext
    {
        public BMSContext(DbContextOptions<BMSContext> options) : base(options)
        {
        }

       protected override void OnModelCreating(ModelBuilder modelBuilder)

{

}
        public static BMSContext connect(string connectionString)
        {
            var optionBuilder = new DbContextOptionsBuilder<BMSContext>();
            optionBuilder.UseMySQL(connectionString);
            optionBuilder.EnableSensitiveDataLogging(true);
            var context = new BMSContext(optionBuilder.Options);
            context.Database.EnsureCreated();
            return context;
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountHolder> AccountHolders { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Overdraft> Overdrafts { get; set; }

    }
}