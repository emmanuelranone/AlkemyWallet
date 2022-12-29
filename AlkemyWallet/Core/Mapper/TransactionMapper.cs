using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Mapper
{
    public static class TransactionMapper
    {
        public static TransactionDTO tMapper (Transaction tEntity)
        {
            TransactionDTO tDTO = new TransactionDTO()
            {
                Amount = tEntity.Amount,
                Concept = tEntity.Concept,
                Date = tEntity.Date,
                Type = tEntity.Type,
                ToAccountId = tEntity.ToAccountId
            };

            return tDTO;
        }
    }
}