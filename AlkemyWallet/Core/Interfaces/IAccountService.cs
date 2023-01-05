using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDTO>> GetAllAsync();
        Task<AccountDTO> GetByIdAsync(int id);
        Task<AccountUpdateDTO> UpdateAsync(int id, AccountUpdateDTO accountDTO);
        PagedList<AccountListDTO> GetAllPage(int page);
        Task<int> Delete(int id);
        Task<TransactionDTO> TransferAsync (TransactionDTO transactionDTO);
        Task<Account> CreateAsync(int id);
        Task<TransactionDTO> DepositAsync(TransactionDTO transactionDTO);
    }
}
