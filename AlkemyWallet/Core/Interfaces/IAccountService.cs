using AlkemyWallet.Core.Models.DTO;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<AccountDTO> GetAll();
    }
}
