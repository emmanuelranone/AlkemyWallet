﻿using AlkemyWallet.Entities;
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
        public virtual DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedRoles();
            modelBuilder.SeedUsers();
            modelBuilder.SeedAccounts();
            modelBuilder.SeedTransactions();
        }
    }
}
