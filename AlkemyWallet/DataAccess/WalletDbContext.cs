using AlkemyWallet.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace AlkemyWallet.DataAccess
{
    public class WalletDbContext : DbContext
    {
        public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed roles acá
            modelBuilder.SeedUsers();
        }
    }
}
