using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlkemyWallet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize("Admin")]
        public IActionResult Get([FromQuery] int? page = 1)
        {
            try
            {
                PagedList<UserListDTO> pageUser = _userService.GetAllPage(page.Value);

                if (page > pageUser.TotalPages)
                {
                    return BadRequest($"page number {page} doesn't exist");
                }
                else
                {
                    var url = this.Request.Path;
                    return Ok(new
                    {
                        next = pageUser.HasNext ? $"{url}/{page + 1}" : "",
                        prev = (pageUser.Count > 0 && pageUser.HasPrevious) ? $"{url}/{page - 1}" : "",
                        currentPage = pageUser.CurrentPage,
                        totalPages = pageUser.TotalPages,
                        data = pageUser
                    });
                }
            }
            catch(Exception ex)
            {
                string error = ex.Message;
                return BadRequest(error);
            }

        }

        [HttpPost]

        public async Task<IActionResult> Register([FromBody] RegisterDTO newUser)
        {
            var userCreated = await _userService.Register(newUser);

            return userCreated != null ? Created("Usuario Creado", userCreated) : BadRequest("There is an user registered whit that email. Please try another one");
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user != null)
                return Ok(user);
            else
                return NotFound();
        }

        [HttpDelete]
        [Authorize("Admin")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var deletedUser = await _userService.Delete(id);
            if (deletedUser != 0)
            {
                return Ok("User " + id + " Deleted");
            }
            else
            {
                return NotFound("User doesn't exist");
            }


        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> Put(int id, UserUpdateDTO dto)
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);
            User result = null;

            if (userId != id)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    result = await _userService.UpdateAsync(id, dto);
                }
                catch
                {
                    return BadRequest();
                }
            }

            if(result != null)
                return Ok("User information updated");
            else 
                return BadRequest();
        }

    }
}