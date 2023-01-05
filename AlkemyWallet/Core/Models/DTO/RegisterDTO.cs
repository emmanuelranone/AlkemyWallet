using System.ComponentModel.DataAnnotations;

namespace AlkemyWallet.Core.Models.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required, EmailAddress, RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required, DataType(DataType.Password), MinLength(8), MaxLength(16)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$", ErrorMessage = "Password must be between 8 and 16 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string Password { get; set; }

    }
}
