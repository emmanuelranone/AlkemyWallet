using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Mapper
{
    public static class TransactionMapper
    {
        public static TransactionDTO TransactionToTransactionDTO (Transaction tEntity)
        {
            TransactionDTO tDTO = new TransactionDTO()
            {
                Amount = tEntity.Amount,
                Concept = tEntity.Concept,
                //Date = tEntity.Date,
                Type = tEntity.Type,
                ToAccountId = tEntity.ToAccountId
            };

            return tDTO;
        }

        public static Transaction TransactionDTOToTransaction (TransactionDetailsDTO tDTO, Transaction tEntity)
        {
            tEntity.Amount = tDTO.Amount;
            tEntity.Concept = tDTO.Concept;
            tEntity.Date = tDTO.Date;
            tEntity.Type = tDTO.Type;
            tEntity.UserId = tDTO.UserId;
            tEntity.AccountId = tDTO.AccountId;
            tEntity.ToAccountId = tDTO.ToAccountId;

            return tEntity;
        }

        public static TransactionDetailsDTO TransactionToTransactionById(Transaction transaction)
        {
            TransactionDetailsDTO transactionDTO = new TransactionDetailsDTO()
            {
                Amount = transaction.Amount,
                Concept = transaction.Concept,
                Date = transaction.Date,
                Type = transaction.Type,
                UserId = transaction.UserId,
                //User = transaction.User,
                AccountId = transaction.AccountId,
                //Account = transaction.Account,
                ToAccountId = transaction.ToAccountId,
                //ToAccount = transaction.ToAccount
            };

            return transactionDTO;
        }
    }
}