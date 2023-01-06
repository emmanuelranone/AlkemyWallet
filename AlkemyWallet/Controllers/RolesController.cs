using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
    [Authorize(Roles = "Admin")]
    /// <summary>
    /// Get all roles. Only available for Administrators.
    /// </summary>
    /// <remarks>
    /// Sample request: api/roles
    /// </remarks>
    /// <returns>List<Roles></returns>
    /// <response code="200">All Roles in order</response>
    /// <response code="401">The client request has not been completed because it lacks valid authentication credentials for the requested resource</response>
    /// <response code="403">When an no admin user try to use the endpoint</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RoleDTO>))]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Nullable))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Get()
    {
        try
        {
            var roles = await _roleService.GetAllAsync();
            if (roles is null)
                return NoContent();

            return Ok(roles);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.BadRequest,
                new
                {
                    message = "Test",
                    errors = new[] { new { error = ex.Message } }
                });
        }
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    /// <summary>
    /// Get all roles for id. Only available for Administrators.
    /// </summary>
    /// <param name="id">idRole</param>
    /// <remarks>
    /// Sample request: api/roles/{id}
    /// </remarks>
    /// <returns>All Roles in order</returns>
    /// <response code="200">All Roles in order</response>
    /// <response code="401">The client request has not been completed because it lacks valid authentication credentials for the requested resource</response>
    /// <response code="403">When an no admin user try to use the endpoint</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RoleDTO>))]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Nullable))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<ErrorHttpCodes>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role is null)
                return NoContent();

            return Ok(role);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
