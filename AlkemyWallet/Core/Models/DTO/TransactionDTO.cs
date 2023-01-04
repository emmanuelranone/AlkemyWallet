using AlkemyWallet.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlkemyWallet.Core.Models.DTO
{
    public class TransactionDTO
    {
        public decimal Amount { get; set; }
        public string Concept { get; set; } 
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }
        public int AccountId { get; set; }
        public int ToAccountId { get; set; }
    }
}