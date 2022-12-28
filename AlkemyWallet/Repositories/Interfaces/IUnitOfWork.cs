using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryBase<Account> AccountRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
