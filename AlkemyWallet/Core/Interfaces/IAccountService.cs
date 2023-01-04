using AlkemyWallet.Core.Models.DTO;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDTO>> GetAllAsync();
        Task<AccountDTO> GetByIdAsync(int id);
        Task<AccountUpdateDTO> UpdateAsync(int id, AccountUpdateDTO accountDTO);
        Task<int> Delete(int id);
    }
}
