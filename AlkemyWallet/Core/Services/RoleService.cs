using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Mapper;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Repositories.Interfaces;
using AutoMapper;

namespace AlkemyWallet.Core.Services;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoleDTO>> GetAllAsync()
    {
        try
        {
            var roles = await _unitOfWork.RoleRepository.GetAllAsync();

            var rolesDTO = new List<RoleDTO>();

            foreach (var role in roles)
            {
                rolesDTO.Add(RoleMapper.RoleToRoleDTO(role));
            }

            return rolesDTO;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        
    }

    public async Task<RoleDTO> GetByIdAsync(int id)
    {
        try
        {
            return _mapper.Map<RoleDTO>(await _unitOfWork.RoleRepository.GetByIdAsync(id));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}