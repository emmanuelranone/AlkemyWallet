using AlkemyWallet.Core.Models.DTO;

namespace AlkemyWallet.Core.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<RoleDTO>> GetAllAsync();
}
