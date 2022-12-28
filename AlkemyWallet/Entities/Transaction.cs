using System.ComponentModel.DataAnnotations.Schema;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Entities
{
    public class Transaction : EntityBase
    {
        [Column("amount", TypeName = "decimal(9,2)")]
        public decimal Amount { get; set; }

        [Column("concept")]
        public string Concept { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("account_id")]
        [ForeignKey("AccountId")]
        public int AccountId { get; set; }
        public Account Account { get; set; }

        [Column("user_id")]
        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [Column("to_account_id")]
        public int ToAccountId { get; set; }

        
        //[ForeignKey("UserId")]
        //public User User { get; set; }        
    }
}