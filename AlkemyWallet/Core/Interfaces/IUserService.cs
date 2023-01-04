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
        Task<int> Delete(int id);
        Task<UserGetByIdDTO> GetByIdAsync(int id);
    }
}
