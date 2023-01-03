using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlkemyWallet.Controllers;

[Route("[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

	public RolesController(IRoleService roleService)
	{
		_roleService = roleService;
	}

    [HttpGet]
    [Authorize("Admin")]
    public async Task<IEnumerable<RoleDTO>> Get()
    {
        return await _roleService.GetAllAsync();
    }

    [HttpGet("{id}")]
    [Authorize("Admin")]
    public IActionResult GetById(int id)
    {
        var role = _roleService.GetByIdAsync(id);
        return role == null ? NotFound($"Role with id: {id} does not exist") : Ok(role);
    }
}
