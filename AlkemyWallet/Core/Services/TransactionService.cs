using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Mapper;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;

namespace AlkemyWallet.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    

        //GET
        public async Task<List<TransactionListDTO>> GetAllAsync()
        {    
            IEnumerable<Transaction> transactions = await _unitOfWork.TransactionRepository.GetAllAsync();

            List<TransactionListDTO> transactionsDTO = new List<TransactionListDTO>();


            IEnumerable<Transaction> result = transactions.OrderByDescending(t => t.Date);

            foreach (var transaction in result)
            {
                transactionsDTO.Add(TransactionMapper.TransactionToTransactionDTO(transaction));
            }

            return transactionsDTO;            
        }

        public async Task<TransactionDetailsDTO> GetById(int id, int UserId)
        {
            var transaction = await _unitOfWork.TransactionRepository.GetFirstOrDefaultAsync(
                t => t.Id == id &&
                t.UserId == UserId, null, "");
            if (transaction == null)
                return null;
            else
                return TransactionMapper.TransactionToTransactionById(transaction);
        }


        // DELETE
        public async Task Delete(int id)
        {
            await _unitOfWork.TransactionRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }        

        public async Task UpdateAsync(int id, TransactionDetailsDTO transactionDTO)
        {
            var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(id);
            
            transaction = TransactionMapper.TransactionDTOToTransaction(transactionDTO, transaction);

            await _unitOfWork.TransactionRepository.UpdateAsync(transaction);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<string> CreateTransactionAsync(int id, TransactionDTO transactionDTO, string type)
        {    
            //FALTA VALIDAR LA EXISTENCIA DE LOS CAMPOS "ENVIADOS" (RECIBIDOS)

            //Almacenamos la cuenta de origen para luego obtener el User_Id correspondiente.
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(id);

            // Un tal Mapper
            var transaction = new Transaction()
            {
                Amount = transactionDTO.Amount,
                Concept = transactionDTO.Concept,
                Date = DateTime.Now,
                Type = type,
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