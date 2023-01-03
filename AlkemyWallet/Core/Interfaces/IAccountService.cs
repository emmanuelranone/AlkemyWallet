using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDTO>> GetAllAsync();
        Task<AccountDTO> GetByIdAsync(int id);
        Task<AccountUpdateDTO> GetByIdUpdateAsync(int id);
        Task<AccountUpdateDTO> UpdateAsync(int id, AccountUpdateDTO accountDTO);
        Task<AccountUpdateDTO> UpdatePatchAsync(AccountUpdateDTO accountDTO, JsonPatchDocument<AccountUpdateDTO> pathDocAccount);
    }
}
