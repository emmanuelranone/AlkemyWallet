using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<IEnumerable<UserDto>> GetAllDtoAsync();
        PagedList<UserListDTO> GetAllPage(int page);
        Task<BriefUserDTO> Register(RegisterDTO newUser);
        Task<int> Delete(int id);
    }
}
