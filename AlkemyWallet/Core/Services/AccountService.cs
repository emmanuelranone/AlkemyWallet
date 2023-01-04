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
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
 

        public AccountService(IUnitOfWork unitOfWork, ITransactionService transactionService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _transactionService = transactionService;
            _mapper = mapper;
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

        public async Task<string> TransferAsync (int id, TransactionDTO transactionDTO)
        {    
            //FALTA VALIDAR EL Nº DE CUENTA CORRESPONDIENTE AL ID DEL USUARIO (TOKEN)

            //Obtenemos la cuenta de origen
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            //Obtenemos la cuenta de destino
            var toAccount = await _unitOfWork.AccountRepository.GetByIdAsync(transactionDTO.ToAccountId);

            //Verificamos si el saldo disponible es mayor al monto de la transferencia
            if (account.Money >= transactionDTO.Amount)
            {
                //Descontamos el saldo según el importe enviado
                account.Money = account.Money - transactionDTO.Amount;
                await _unitOfWork.AccountRepository.UpdateAsync(account);
            };

            //Ingresamos el importe a la cuenta de destino
            toAccount.Money = toAccount.Money + transactionDTO.Amount;
            await _unitOfWork.AccountRepository.UpdateAsync(toAccount);

            var transaction = new Transaction()
            {
                Amount = transactionDTO.Amount,
                Concept = transactionDTO.Concept,
                Date = DateTime.Now,
                Type = "Transferencia",
                UserId = account.User_Id,
                AccountId = id,
                ToAccountId = transactionDTO.ToAccountId
            };

            await _unitOfWork.TransactionRepository.AddAsync(transaction);

            await _unitOfWork.SaveChangesAsync();

            return "";
        }
    }
}
