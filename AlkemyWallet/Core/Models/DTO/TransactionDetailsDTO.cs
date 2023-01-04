using AlkemyWallet.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Core.Models.DTO
{
    public class TransactionDetailsDTO
    {
        public decimal Amount { get; set; }
        public string Concept { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public int AccountId { get; set; }
        public double Account { get; set; }
        public int ToAccountId { get; set; }
        public double ToAccount { get; set; }
    }
}
