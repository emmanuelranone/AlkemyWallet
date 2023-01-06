using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AlkemyWallet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize("Admin, Regular")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountsController(IAccountService accountService, IHttpClientFactory httpClientFactory)
        {
            _accountService = accountService;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Get a paginated list of accounts. Only available for Administrators.
        /// </summary>
        /// <returns>Return a paginated account list</returns>
        /// <response code="200">Success request</response>
        /// <response code="401">User not authenticated</response>
        /// <response code="403">User with no valid credentials</response>
        /// <response code="404">Source not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<AccountListDTO>))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Get([FromQuery] int? page = 1)
        {
            try
            {
                PagedList<AccountListDTO> pageAccount = _accountService.GetAllPage(page.Value);

                if (page > pageAccount.TotalPages)
                {
                    return BadRequest($"page number {page} doesn't exist");
                }
                else
                {
                    var url = this.Request.Path;
                    return Ok(new
                    {
                        next = pageAccount.HasNext ? $"{url}?page={page + 1}" : "",
                        prev = (pageAccount.Count > 0 && pageAccount.HasPrevious) ? $"{url}?page={page - 1}" : "",
                        currentPage = pageAccount.CurrentPage,
                        totalPages = pageAccount.TotalPages,
                        data = pageAccount
                    });
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return BadRequest(error);
            }
        }

        /// <summary>
        /// Get an account by id. Only available for Administrators.
        /// </summary>
        /// <returns>The account requested by id</returns>
        /// <response code="200">Success request</response>
        /// <response code="401">User not authenticated</response>
        /// <response code="403">User with no valid credentials</response>
        /// <response code="404">Source not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<AccountDTO>))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Regular")]
        public async Task<AccountDTO> Get(int id)
        {
            return await _accountService.GetByIdAsync(id);
        }

        /// <summary>
        /// Create an account. Only available for Regular user.
        /// </summary>
        /// <returns>OkResult object if succeed</returns>
        /// <response code="200">Success request</response>
        /// <response code="401">User not authenticated</response>
        /// <response code="403">User with no valid credentials</response>
        /// <response code="404">Source not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResult))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> Post()
        {
            var id = int.Parse(User.FindFirst("UserId").Value);

            var result = await _accountService.CreateAsync(id);

            if (result != null)
                return Ok(result);
             
            return BadRequest();
        }

        /// <summary>
        /// Update if user is locked or not. Only available for Administrators.
        /// </summary>
        /// <returns>Return a modified account object</returns>
        /// <response code="200">Success request</response>
        /// <response code="401">User not authenticated</response>
        /// <response code="403">User with no valid credentials</response>
        /// <response code="404">Source not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountUpdateDTO))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PatchAsync(int id, AccountUpdateDTO accountDTO)
        {
            var updatedAccount = await _accountService.UpdateAsync(id, accountDTO);
            if (updatedAccount == null)
                return NotFound();
            
            return Ok(updatedAccount);
        }

        /// <summary>
        /// Delete an account by id. Only available for Administrators.
        /// </summary>
        /// <returns>No content returned</returns>
        /// <response code="200">Success request</response>
        /// <response code="401">User not authenticated</response>
        /// <response code="403">User with no valid credentials</response>
        /// <response code="404">Source not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedUser = await _accountService.Delete(id);
            if (deletedUser > 0)
                return NoContent();
            else
                return NotFound();
        }

        /// <summary>
        /// Make a deposit or transaction. Only available for Regular users.
        /// </summary>
        /// <returns>Return a transaction object</returns>
        /// <response code="200">Success request</response>
        /// <response code="401">User not authenticated</response>
        /// <response code="403">User with no valid credentials</response>
        /// <response code="404">Source not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionDTO))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("{id}")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> TransactionAsync(int id, TransactionDTO transactionDTO)
        {
            if (transactionDTO.Amount >= (decimal)0.01)
            {
                //Obtenemos la account del id ingresado en el path
                var account = await _accountService.GetByIdAsync(id);
                //Obtenemos el User_id del Token de la cuenta logueada
                var userId = int.Parse(User.FindFirst("UserId").Value);

                transactionDTO.UserId = userId;
                transactionDTO.Date = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                transactionDTO.AccountId = id;

               
                //String respuesta de la tarea realizada
                var responseString = transactionDTO;

                if (userId == account.User_Id)
                {
                    if (transactionDTO.Type.ToString() == "Transferencia")
                    {
                        responseString = await _accountService.TransferAsync(transactionDTO);                        
                    }
                    else if (transactionDTO.Type.ToString() == "Deposito")
                    {                        
                        responseString = await _accountService.DepositAsync(transactionDTO);
                    }
                    else
                    {
                        return BadRequest("Type of transaction doesn't exist");
                    }

                    if (responseString != null)
                    {
                        //log of the transaction on Endpoint

                        var httpClient = _httpClientFactory.CreateClient("Myurl");
                        var launchUrl = LaunchUrl.GetApplicationUrl();

                        using var httpResponseMessage =
                            await httpClient.PostAsJsonAsync(launchUrl + "/transactions", transactionDTO);

                        var data = await httpResponseMessage.Content.ReadAsStringAsync();

                        return Ok(responseString);
                    }

                    return BadRequest("Please verify destination account");
                    
                }

                return BadRequest("Account does not belong to user.");
            }
            return BadRequest("Amount must be greater than 0,01");
            
        }

        }
}
