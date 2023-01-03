using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;

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

        //public static AccountUpdateDTO AccountToAccountUpdateDTO(Account account)
        //{
        //    AccountUpdateDTO accountDTO = new AccountUpdateDTO()
        //    {
        //        IsBlocked = account.IsBlocked
        //    };
        //    return accountDTO;
        //}

        //public static Account AccountUpdateDTOToAccount(AccountUpdateDTO accountDTO, Account account)
        //{
        //    account.IsBlocked = accountDTO.IsBlocked;
        //    return account;
        //}

    }
}
