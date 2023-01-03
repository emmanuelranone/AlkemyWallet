using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Core.Models.DTO
{
    public class UserListDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
