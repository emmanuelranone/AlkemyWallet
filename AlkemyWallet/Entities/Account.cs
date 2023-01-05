using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Entities
{
    public class Account : EntityBase
    {
        [Column("creationDate")]
        public DateTime CreationDate { get; set; }
        [Column("money", TypeName = "decimal(9, 2)")]
        public decimal Money { get; set; }
        [Column("isBlocked")]
        public bool IsBlocked { get; set; }
        [ForeignKey("User"), Column("userId")]
        public int User_Id { get; set; }
        public User User { get; set; }
    }
}
