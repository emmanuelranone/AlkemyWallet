using AlkemyWallet.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Core.Models.DTO
{
    public class AccountDTO
    {        
        public decimal Money { get; set; }
        [ForeignKey("User")]
        public int User_Id { get; set; }
        public User User { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsBlocked { get; set; }
    }
}
