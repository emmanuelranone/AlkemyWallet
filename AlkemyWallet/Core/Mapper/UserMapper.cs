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
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Points = user.Points,
                RoleId = user.RoleId
            };
            return userDTO;
        }
    }
}
