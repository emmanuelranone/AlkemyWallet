using AlkemyWallet.Core.Models.DTO;
using eWallet_API.Entities;

namespace AlkemyWallet.Core.Mapper
{
    public static class AccountMapper
    {

        public static AccountDTO AccountToAccountDTO(this Account account)
        {
            AccountDTO categoryDTO = new AccountDTO()
            {
                Money = account.Money,
                User_Id = account.User_Id,
                User = account.User,
                CreationDate = account.CreationDate,
                IsBlocked = account.IsBlocked
            };
            return categoryDTO;
        }

    }
}
