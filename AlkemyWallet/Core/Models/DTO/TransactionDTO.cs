using AlkemyWallet.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlkemyWallet.Core.Models.DTO
{
    public class TransactionDTO
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Concept { get; set; }

        public int UserId { get; set; }

        [Required]
        public int ToAccountId { get; set; }

        [Required, EnumDataType(typeof(TransactionTypes))]
        public string Type { get; set; }
        public DateTime Date { get; set; }       
        public int AccountId { get; set; }
    }

    public enum TransactionTypes
    {
        Transferencia = 1,
        Deposito = 2
    }
}