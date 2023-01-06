using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Mapper;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using AutoMapper;

namespace AlkemyWallet.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //private readonly ITransactionService _transactionService;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)// ITransactionService transactionService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //_transactionService = transactionService;
        }

        // GET
        public async Task<IEnumerable<AccountDTO>> GetAllAsync()
        {
            var accounts = await _unitOfWork.AccountRepository.GetAllAsync();
            var accountsDTO = new List<AccountDTO>();

            foreach (var account in accounts)
            {
                accountsDTO.Add(AccountMapper.AccountToAccountDTO(account));
            }

            return accountsDTO;

        }

        // GET
        public async Task<AccountDTO> GetByIdAsync(int id)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            var accountDTO = new AccountDTO();

            accountDTO = AccountMapper.AccountToAccountDTO(account);
            return accountDTO;
        }

        public async Task<AccountUpdateDTO> UpdateAsync(int id, AccountUpdateDTO accountDTO)
        {
            try
            {
                var existAccount = await _unitOfWork.AccountRepository.GetByIdAsync(id);
                if (existAccount == null)
                    return null;

                var accountToUpdate = AccountMapper.UpdateDTOToAccount(accountDTO, existAccount);

                var accountUpdate = _unitOfWork.AccountRepository.UpdateAsync(accountToUpdate);
                await _unitOfWork.SaveChangesAsync();

                var account = _mapper.Map<AccountUpdateDTO>(accountToUpdate);
                
                return account;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PagedList<AccountListDTO> GetAllPage(int page = 1)
        {
            if (page < 1) throw new ArgumentException("Argument must be greater than 0", "page");

            var accounts = _unitOfWork.AccountRepository.GetAllPaged(page, 10);

            var accountsDTO = _mapper.Map<List<AccountListDTO>>(accounts);

            var pagedAccounts = new PagedList<AccountListDTO>(accountsDTO, accounts.TotalCount, page);
            
            return pagedAccounts;
        }

        public async Task<int> Delete(int id)
        {
            var deleted = await _unitOfWork.AccountRepository.Delete(id);
            if (deleted > 0)
            {
                _unitOfWork.SaveChangesAsync();
                return id;
            }
            else
                return 0;
        }

        public async Task<TransactionDTO> TransferAsync (TransactionDTO transactionDTO)
        {    
            var accountOrigin = await _unitOfWork.AccountRepository.GetByIdAsync(transactionDTO.AccountId);
            var accountDestiny = await _unitOfWork.AccountRepository.GetByIdAsync(transactionDTO.ToAccountId);
            
            if (accountDestiny is null || accountDestiny.Id == accountOrigin.Id)
                return null;

            if (accountOrigin.Money >= transactionDTO.Amount)
            {
                accountOrigin.Money -= transactionDTO.Amount;
                await _unitOfWork.AccountRepository.UpdateAsync(accountOrigin);

                accountDestiny.Money += transactionDTO.Amount;
                await _unitOfWork.AccountRepository.UpdateAsync(accountDestiny);
            } 
            else
                return null;

            await _unitOfWork.SaveChangesAsync();

            return transactionDTO;
        }

        public async Task<Account> CreateAsync(int id)
        {
            var account = new Account 
            {
                CreationDate = DateTime.Now,
                IsBlocked = false,
                Money = 0,
                User_Id = id
            };

            account =   await _unitOfWork.AccountRepository.AddAsync(account);
                        await _unitOfWork.SaveChangesAsync();

            return account;
        }

        public async Task<TransactionDTO> DepositAsync(TransactionDTO transactionDTO)
        {
            try
            {
                if (transactionDTO.ToAccountId == transactionDTO.AccountId)
                {

                    //bring the account data by Id
                    var account = await _unitOfWork.AccountRepository.GetByIdAsync(transactionDTO.AccountId);

                    //Add the amount of money made by the transaction
                    account.Money += transactionDTO.Amount;
                    await _unitOfWork.AccountRepository.UpdateAsync(account);

                    await _unitOfWork.SaveChangesAsync();

                    return transactionDTO;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
