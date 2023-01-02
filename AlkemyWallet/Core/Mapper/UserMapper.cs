using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Mapper
{
    public static class UserMapper
    {
        public static UserListDTO userToUsserListDTO (this User user)
        {
            UserListDTO userDTO = new UserListDTO()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,               
                RoleId = user.RoleId
            };
            return userDTO;
        }
    }
}
