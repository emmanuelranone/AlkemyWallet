using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Mapper;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using AutoMapper;

namespace AlkemyWallet.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        // GET
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

        public PagedList<TransactionPagedDTO> GetAllPage(int page = 1)
        {
            if (page < 1) 
                throw new ArgumentException("Argument must be greater than 0", "page");

            var transactions = _unitOfWork.TransactionRepository.GetAllPaged(page, 10);

            var transactionsDTO = _mapper.Map<List<TransactionPagedDTO>>(transactions);

            var pagedTransactions = new PagedList<TransactionPagedDTO>(transactionsDTO, transactions.TotalCount, page);
            return pagedTransactions;
        }


        // DELETE
        public async Task Delete(int id)
        {
            await _unitOfWork.TransactionRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
        }        

        // UPDATE
        public async Task UpdateAsync(int id, TransactionDetailsDTO transactionDTO)
        {
            var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(id);
            
            transaction = TransactionMapper.TransactionDTOToTransaction(transactionDTO, transaction);

            await _unitOfWork.TransactionRepository.UpdateAsync(transaction);
            await _unitOfWork.SaveChangesAsync();
        }

        // POST
        public async Task<TransactionDTO> CreateTransactionAsync(TransactionDTO transactionDTO)
        {    
            try
            {
                var transaction = TransactionMapper.TransactionDTOToTransaction(transactionDTO);

                await _unitOfWork.TransactionRepository.AddAsync(transaction);
                await _unitOfWork.SaveChangesAsync();

                return transactionDTO;        
            }
            catch
            {
                return null;
            }        
        }
    }    
}