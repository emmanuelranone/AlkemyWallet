using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Mapper
{
    public static class UserMapper
    {
        public static AuthMeDTO UserToAuthMeDTO(this User user)
        {
            AuthMeDTO authMeDTO = new AuthMeDTO()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Points = user.Points,
                Role = user.Role.Name
            };
            return authMeDTO;
        }

        public static User UserUpdateDtoToUser(UserUpdateDTO dto, User u)
        {
            if (dto.FirstName != null)
                u.FirstName = dto.FirstName;

            if (dto.LastName != null)
                u.LastName = dto.LastName;

            if (dto.Password != null)
                u.Password = dto.Password;

            return u;
        }


        //public static UserListDTO userToUsserListDTO (this User user)
        //{
        //    UserListDTO userDTO = new UserListDTO()
        //    {
        //        Id = user.Id,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        Email = user.Email,               
        //        RoleId = user.RoleId
        //    };
        //    return userDTO;
        //}
    }
}
