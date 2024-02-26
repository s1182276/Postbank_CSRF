using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Postbank.Models;

namespace Postbank.Data
{
    public class PostbankContext : DbContext
    {
        public PostbankContext (DbContextOptions<PostbankContext> options)
            : base(options)
        {
        }

        public DbSet<Postbank.Models.BankUser> BankUsers { get; set; } = default!;
        public DbSet<Postbank.Models.BankAccount> BankAccounts { get; set; } = default!;
        public DbSet<Postbank.Models.BankTransaction> BankTransactions { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankUser>()
            .HasOne(a => a.BankAccount)
                    .WithOne(u => u.BankUser)
            .HasForeignKey<BankAccount>(i => i.BankUserId)
            .IsRequired();
        }
    }
}
