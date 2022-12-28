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
    }
}
