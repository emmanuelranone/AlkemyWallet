using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AutoMapper;

namespace AlkemyWallet.Core.Mapper
{
    public class EntityMapper : Profile
    {
        public EntityMapper() 
        {
            //RolMaps
            CreateMap<Role, RoleDTO>();
            CreateMap<RoleDTO, Role>();

            //UserMaps
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserListDTO>();
            CreateMap<UserListDTO, User>();
            CreateMap<User, BriefUserDTO>();
            CreateMap<User, UserGetByIdDTO>();
            CreateMap<UserGetByIdDTO, User>();

            //AccountMaps
            CreateMap<AccountListDTO, Account>();
            CreateMap<Account, AccountListDTO>();
            CreateMap<AccountUpdateDTO, Account>();
            CreateMap<Account, AccountUpdateDTO>();
            CreateMap<AccountUpdateDTO, AccountDTO>();
            CreateMap<AccountDTO, AccountUpdateDTO>();
        }
    }
}
