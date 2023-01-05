using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.Text;
using System;
using Azure;

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
        public async Task<IActionResult> TransactionAsync(int id, TransactionDTO transactionDTO)
        {
            if (transactionDTO.Amount >= (decimal)0.01)
            {
                var httpClient = _httpClientFactory.CreateClient("Myurl");
                var launchUrl = LaunchUrl.GetApplicationUrl();
                

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
