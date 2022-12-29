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
            TransactionDTO tDTO = new TransactionDTO();

            tDTO.Amount = tEntity.Amount;
            tDTO.Concept = tEntity.Concept;
            tDTO.Date = tEntity.Date;
            tDTO.Type = tEntity.Type;
            tDTO.ToAccountId = tEntity.ToAccountId;

            return tDTO;
        }
    }
}