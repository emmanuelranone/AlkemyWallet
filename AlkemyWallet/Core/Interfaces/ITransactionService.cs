using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Interfaces
{
    public interface ITransactionService
    {
        Task <List<TransactionDTO>> GetAllAsync();
        Task<TransactionDTO> GetById(int id, int UserId);
    }
}