using AlkemyWallet.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlkemyWallet.DataAccess
{
    public static class WalletSeeds
    {
        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Email = "user1@gmail.com", Password = "Password@123", RoleId = 1 },
                new User { Id = 2, Email = "user2@gmail.com", Password = "Password@123", RoleId = 2 }
                );
        }

        public static void SeedTransaction(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().HasData(
                new Transaction()
                { 
                    Id = 1, 
                    Amount = 1234.32m, 
                    Concept = "Varios",
                    Date = DateTime.Parse("10/12/2022"),
                    Type = "payment", 
                    Account_id = 1,
                    User_id = 1,
                    To_account_id = 2
                },
                new Transaction()
                { 
                    Id = 2, 
                    Amount = 4566.12m, 
                    Concept = "Varios",
                    Date = DateTime.Parse("15/12/2022"),
                    Type = "payment", 
                    Account_id = 2,
                    User_id = 2,
                    To_account_id = 1
                }
            );
        }
    }
}
