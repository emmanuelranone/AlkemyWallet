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

            transactions.OrderByDescending(t => t.Date).ToList();

            List<TransactionDTO> transactionsDTO = new List<TransactionDTO>();

            foreach (var transaction in transactions)
            {
                transactionsDTO.Add(TransactionMapper.tMapper(transaction));
            }

            return transactionsDTO;

            /*
            List<Transaction> transactions = await _unitOfWork.TransactionRepository.GetAllAsync();

            IEnumerable<Transaction> result = transactions.OrderByDescending(t => t.Date);

            List<TransactionDTO> transactionsDTO = new List<TransactionDTO>();

            foreach (var transaction in result)
            {
                transactionsDTO.Add(TransactionMapper.tMapper(result));
            }

            return transactionsDTO;
            */
            
        }
    }    
}