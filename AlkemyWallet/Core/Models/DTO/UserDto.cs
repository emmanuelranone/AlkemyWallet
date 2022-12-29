using AlkemyWallet.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AlkemyWallet.Core.Models.DTO
{
    public class UserDto
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
    }
}
