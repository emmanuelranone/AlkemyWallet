using AlkemyWallet.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Core.Models.DTO
{
    public class AccountListDTO
    {
        public DateTime CreationDate { get; set; }
        public double Money { get; set; } // dato sensible?
        public bool IsBlocked { get; set; }
        public int User_Id { get; set; }
    }
}
