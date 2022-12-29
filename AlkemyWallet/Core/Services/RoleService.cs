using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Mapper;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Repositories.Interfaces;

namespace AlkemyWallet.Core.Services;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;

    public RoleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<RoleDTO>> GetAllAsync()
    {
        var roles = await _unitOfWork.RoleRepository.GetAllAsync();
        //return roles.Cast<RoleDTO>().ToList();

        var rolesDTO = new List<RoleDTO>();

        foreach (var role in roles)
        {
            rolesDTO.Add(RoleMapper.RoleToRoleDTO(role));
        }

        return rolesDTO;
    }
}