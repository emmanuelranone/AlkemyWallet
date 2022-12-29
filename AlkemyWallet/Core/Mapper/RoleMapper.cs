using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Mapper;

public static class RoleMapper
{
    public static RoleDTO RoleToRoleDTO(this Role role)
    {
        RoleDTO roleDTO = new RoleDTO()
        {
            Name = role.Name,
            Description = role.Description
        };
        return roleDTO;
    }
}