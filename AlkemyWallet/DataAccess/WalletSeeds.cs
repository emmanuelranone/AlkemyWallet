using AlkemyWallet.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlkemyWallet.DataAccess
{
    public static class WalletSeeds
    {
        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", Description = "Usuario Administrador" },
                new Role { Id = 2, Name = "Regular", Description = "Usuario Regular" }
                );
        }

        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Name1",
                    LastName = "LastName1",
                    Email = "user1@gmail.com",
                    Password = "Password@123",
                    RoleId = 1
                },
                new User
                {
                    Id = 2,
                    FirstName = "Name2",
                    LastName = "LastName2",
                    Email = "user2@gmail.com",
                    Password = "Password@123",
                    RoleId = 2
                }
                );
        }

        public static void SeedAccounts(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
                new Account { Id = 1, User_Id = 1, CreationDate = DateTime.Now, IsBlocked = false, Money = 10000.00m },
                new Account { Id = 2, User_Id = 2, CreationDate = DateTime.Now, IsBlocked = false, Money = 20000.00m},
                new Account { Id = 3, User_Id = 1, CreationDate = DateTime.Now, IsBlocked = false, Money = 5000.00m },
                new Account { Id = 4, User_Id = 2, CreationDate = DateTime.Now, IsBlocked = false, Money = 2000.00m }
                );

            // For pagination test

            //List<Account> accountList = new();

            //for (int i = 1; i <= 15; i++)
            //{
            //    accountList.Add(
            //        new Account
            //        {
            //            Id = i,
            //            User_Id = 1,
            //            CreationDate = DateTime.Now,
            //            IsBlocked = false,
            //            Money = 10000.00
            //        }
            //    );
            //}

            //for (int i = 16; i <= 30; i++)
            //{
            //    accountList.Add(
            //        new Account
            //        {
            //            Id = i,
            //            User_Id = 2,
            //            CreationDate = DateTime.Now,
            //            IsBlocked = false,
            //            Money = 10000.00
            //        }
            //    );
            //}

            //modelBuilder.Entity<Account>().HasData(accountList.ToArray());

        }

        public static void SeedTransactions(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().HasData(
                new Transaction
                {
                    Id = 1,
                    Amount = 1234.32m,
                    Concept = "Varios",
                    Date = DateTime.Parse("10/12/2022"),
                    Type = "payment",
                    AccountId = 1,
                    UserId = 1,
                    ToAccountId = 2
                },
                new Transaction
                {
                    Id = 2,
                    Amount = 4566.12m,
                    Concept = "Varios",
                    Date = DateTime.Parse("15/12/2022"),
                    Type = "payment",
                    AccountId = 3,
                    UserId = 2,
                    ToAccountId = 4
                }
            );
        }
    }
}
