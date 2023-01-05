using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Interfaces
{
    public interface ITransactionService
    {
        Task <List<TransactionListDTO>> GetAllAsync();
        Task<TransactionDetailsDTO> GetById(int id, int userId);
        PagedList<TransactionPagedDTO> GetAllPage(int page);
        Task Delete(int id);
        Task UpdateAsync(int id, TransactionDetailsDTO transactionDTO);
        Task<TransactionDTO> CreateTransactionAsync(TransactionDTO transactionDTO);
    }
}