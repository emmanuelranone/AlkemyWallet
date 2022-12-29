using AlkemyWallet.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AlkemyWallet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [HttpGet]
        [Route("usersDto")]
        public async Task<IActionResult> GetUsersDto()
        {
            return Ok(await _userService.GetAllDtoAsync());
        }
    }
}