using AlkemyWallet.DataAccess;
using AlkemyWallet.Repositories.Interfaces;
using AlkemyWallet.Entities;
using System.Diagnostics;

namespace AlkemyWallet.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WalletDbContext _dbContext;

        private readonly IRepositoryBase<Account> _accountRepository;
        private readonly IRepositoryBase<Transaction> _transactionRepository;

        public IRepositoryBase<Account> AccountRepository => _accountRepository ?? new RepositoryBase<Account>(_dbContext);
        public IRepositoryBase<Transaction> TransactionRepository => _transactionRepository ?? new RepositoryBase<Transaction>(_dbContext);

        public UnitOfWork(WalletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
