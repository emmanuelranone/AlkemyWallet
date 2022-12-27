using AlkemyWallet.DataAccess;
using AlkemyWallet.Repositories.Interfaces;
using eWallet_API.Entities;
using System.Diagnostics;

namespace AlkemyWallet.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WalletDbContext _dbContext;

        private readonly IRepositoryBase<Account> _accountRepository;

        public IRepositoryBase<Account> AccountRepository => _accountRepository ?? new RepositoryBase<Account>(_dbContext);

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
