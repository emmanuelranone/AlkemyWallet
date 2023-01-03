using System.ComponentModel.DataAnnotations;

namespace AlkemyWallet.Core.Models.DTO
{
    public class AuthMeDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? Points { get; set; }
        public string Role { get; set; }
    }
}
