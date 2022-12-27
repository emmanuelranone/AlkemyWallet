using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AlkemyWallet.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : EntityBase
    {
        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("email")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Column("password")]
        [Required]
        public string Password { get; set; }

        [Column("points")]
        public int? Points { get; set; }

        [Column("rol_Id")]
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
