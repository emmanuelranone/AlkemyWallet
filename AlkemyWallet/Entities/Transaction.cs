using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlkemyWallet.Entities
{
    public class Transaction : EntityBase
    {
        public decimal Amount { get; set; }
        public string Concept { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int Account_id { get; set; }
        public int User_id { get; set; }
        public int To_account_id { get; set; }

        
        [ForeignKey("Account_id")]
        public virtual Account account { get; set; }
        [ForeignKey("User_id")]
        public virtual User user { get; set; }        
    }
}