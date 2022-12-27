using eWallet_API.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryBase<Account> AccountRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
