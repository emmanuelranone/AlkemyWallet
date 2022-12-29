using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<List<TransactionDTO>> GetAllAsync()
        {    
            IEnumerable<Transaction> transactions = await _unitOfWork.TransactionRepository.GetAllAsync();

            List<TransactionDTO> transactionsDTO = new List<TransactionDTO>();


            IEnumerable<Transaction> result = transactions.OrderByDescending(t => t.Date);

            foreach (var transaction in result)
            {
                transactionsDTO.Add(TransactionMapper.tMapper(transaction));
            }

            return transactionsDTO;            
        }
    }    
}