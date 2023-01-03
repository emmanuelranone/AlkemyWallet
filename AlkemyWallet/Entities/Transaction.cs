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

        [Column("user_id")]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Column("account_id")]
        [ForeignKey("Account")]
        public int AccountId { get; set; }

        public virtual Account Account { get; set; }

        [Column("to_account_id")]
        [ForeignKey("ToAccount")]
        public int ToAccountId { get; set; }

        public virtual Account ToAccount { get; set; }
    }
}