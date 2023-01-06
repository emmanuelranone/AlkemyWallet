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


        /// <summary>
        ///    Paged list of all registered users. Only available for Administrators.
        /// </summary>
        /// <remarks>
        /// Sample request: /users
        /// </remarks>
        /// <returns>List of registered users.</returns>
        /// /// <response code="200"> Shows the list of Users.</response>
        /// /// <response code="400"> Shown when page doesn't exist.</response>
        /// /// <response code="401">If the user is not an administrator try to run the endpoint.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<UserListDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// Register a new User
        /// </summary>
        /// <param name="register">User Information</param>
        /// <returns> Information of register User</returns>
        /// <remarks>
        /// Sample Request:      All Parameters required           
        ///                 /api/Auth/Register
        ///                 {
        ///                     "firstName": "User Name",
        ///                     "lastName": "User LastName",
        ///                     "email": "user@example.com",
        ///                     "password": "Password"
        ///                 }
        /// </remarks>
        /// ///<response code="201">Created for private Endpoints</response>
        /// /// <response code="400">If the Register users data is incorrect or a required field is missing</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BriefUserDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterDTO newUser)
        {
            var userCreated = await _userService.Register(newUser);

            return userCreated != null ? Created("Usuario Creado", userCreated) : BadRequest("There is an user registered whit that email. Please try another one");
        }

        /// <summary>
        ///    Return information of register user. Only available for Regular users.
        /// </summary>
        /// <remarks>
        /// Sample request: /users/5
        /// </remarks>
        /// <returns>Info of registered user.</returns>
        /// /// <response code="200"> Shows the User information.</response>
        /// /// <response code="401"> If other than the user try to run the endpoint.</response>
        /// /// <response code="404"> Shown when user doesn't exist.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserGetByIdDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Delete an existing User. Only available for Administrators.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> return id of user deleted</returns>
        /// <remarks>
        /// Sample Request:
        ///     Delete /users/2
        /// </remarks>
        /// /// <response code="200">If User Was deleted</response>
        /// /// <response code="401">If User is not an admin</response>
        /// /// <response code="404">If User does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Update an existing User. Only available for Regular users.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> "Updated User"</returns>
        /// <remarks>
        /// Sample Request: 
        ///     PUT /User/1
        ///     {
        ///        "FirstName": "Name user.",
        ///        "LastName": "LastName user.",
        ///        "Password": "abcdefgh" -- "12345678"
        ///     }
        /// </remarks>
        /// /// <response code="200"> User was successfully updated.</response>
        /// /// <response code="400"> information for update it is not valid</response>
        /// /// <response code="403"> User has not permission for update other users.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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