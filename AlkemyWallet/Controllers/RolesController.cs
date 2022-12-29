using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AlkemyWallet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

	public RolesController(IRoleService roleService)
	{
		_roleService = roleService;
	}

    [HttpGet]
    //[Authorize("Admin")]
    public async Task<IEnumerable<RoleDTO>> Get()
    {
        return await _roleService.GetAllAsync();
    }
}
