using AlkemyWallet.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Core.Models.DTO
{
    public class AccountListDTO
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int User_Id { get; set; }
    }
}
