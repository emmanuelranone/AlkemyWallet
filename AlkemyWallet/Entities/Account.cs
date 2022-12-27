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
        [ForeignKey("User")]
        public int User_Id { get; set; }
        public virtual User User { get; set; }
    }
}
