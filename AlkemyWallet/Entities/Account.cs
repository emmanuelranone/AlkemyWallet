using AlkemyWallet.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace eWallet_API.Entities
{
    public class Account : EntityBase
    {
        [Column("creationDate")]
        public DateTime CreationDate { get; set; }
        [Column("money")]
        public double Money { get; set; }
        [Column("isBlocked")]
        public bool IsBlocked { get; set; }
        [ForeignKey("User"), Column("userId")]
        public int User_Id { get; set; }
        public User User { get; set; }
    }
}
