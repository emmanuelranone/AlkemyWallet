using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        [Authorize("Admin")]
        public async Task<AccountDTO> Get(int id)
        {
            return await _accountService.GetByIdAsync(id);
        }

        [HttpPost]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> Post()
        {
            var id = int.Parse(User.FindFirst("UserId").Value);

            var result = await _accountService.CreateAsync(id);

            if (result != null)
                return Ok();
             
            return BadRequest();
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PatchAsync(int id, AccountUpdateDTO accountDTO)
        {
            var updatedAccount = await _accountService.UpdateAsync(id, accountDTO);
            if (updatedAccount == null)
                return NotFound();
            
            return Ok(updatedAccount);
        }

        [HttpDelete("{id}")]
        //[ProducesResponseType((int)HttpStatusCode.NoContent)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedUser = await _accountService.Delete(id);
            if (deletedUser > 0)
                return NoContent();
            else
                return NotFound();
        }

        // POST api/<AccountController>/5
        [HttpPost("{id}")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> TransactionAsync (int id, TransactionDTO transactionDTO)
        {   
            var accountOrigin = await _accountService.GetByIdAsync(id);
            //Obtenemos el User_id del Token de lac uenta logueada
            var userId = int.Parse(User.FindFirst("UserId").Value);
            
            transactionDTO.UserId = userId; 

            var result = transactionDTO; /// ver

            if (userId == accountOrigin.User_Id)
            {
                //Transferencia
                if (transactionDTO.ToAccountId != id)
                {   
                    transactionDTO.AccountId = id;
                    result = await _accountService.TransferAsync(transactionDTO);
                }
                else
                {
                    //Depósito
                    //await _accountService.DepositAsync(userId, id, transactionDTO);
                }
            }
            var launchUrl = LaunchUrl.GetApplicationUrl();

            var client = _httpClientFactory.CreateClient("transactions");
            var response = await client.PostAsJsonAsync(launchUrl + "/transactions", result);
            var data = await response.Content.ReadAsStringAsync();
            return Ok(data);
        }
    }
}
