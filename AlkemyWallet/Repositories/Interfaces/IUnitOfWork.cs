using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryBase<Role> RoleRepository { get; }
        IRepositoryBase<User> UserRepository { get; }
        IRepositoryBase<Account> AccountRepository { get; }
        IRepositoryBase<Transaction> TransactionRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
