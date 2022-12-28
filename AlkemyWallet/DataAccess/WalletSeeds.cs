using AlkemyWallet.Entities;
using eWallet_API.Entities;
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

        public static void SeedAccounts(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
                new Account { Id = 1, User_Id = 1, CreationDate = DateTime.Now, IsBlocked = false, Money = 10000.00 },
                new Account { Id = 2, User_Id = 2, CreationDate = DateTime.Now, IsBlocked = false, Money = 20000.00 },
                new Account { Id = 3, User_Id = 1, CreationDate = DateTime.Now, IsBlocked = false, Money = 5000.00 },
                new Account { Id = 4, User_Id = 2, CreationDate = DateTime.Now, IsBlocked = false, Money = 2000.00 }
                );
        }
    }
}
