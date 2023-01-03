using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlkemyWallet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<string> Post(LoginDTO loginDTO)
        {
            return await _authService.Login(loginDTO.Email, loginDTO.Password);
        }

        [HttpGet("me")]
        [Authorize(Roles = "Admin, Regular")]
        public async Task<AuthMeDTO> Me()
        {
            var id = int.Parse(User.FindFirst("UserId").Value);

            return await _authService.GetAuthMeAsync(id);
        }
    }
}
