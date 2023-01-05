using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDTO>> GetAllAsync();
        Task<AccountDTO> GetByIdAsync(int id);
        Task<AccountUpdateDTO> UpdateAsync(int id, AccountUpdateDTO accountDTO);
        Task<Account> CreateAsync(int id);
    }
}
