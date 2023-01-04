using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
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

    }
}