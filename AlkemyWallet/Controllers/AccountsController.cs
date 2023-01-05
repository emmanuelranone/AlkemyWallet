using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Core.Services;
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

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: api/<AccountController>
        [HttpGet]
        [Authorize("Admin")]
        public async Task<IEnumerable<AccountDTO>> Get()
        {
            return await _accountService.GetAllAsync();
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        [Authorize("Admin")]
        public async Task<AccountDTO> Get(int id)
        {
            return await _accountService.GetByIdAsync(id);
        }

        //// POST api/<AccountController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

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
                //Obtenemos la account del id ingresado en el path
                var account = await _accountService.GetByIdAsync(id);
                //Obtenemos el User_id del Token de la cuenta logueada
                var userId = int.Parse(User.FindFirst("UserId").Value);

                transactionDTO.UserId = userId;

                //String respuesta de la tarea realizada
                string response;

                if (userId == account.User_Id)
                {
                    if (transactionDTO.Type == "Transferencia")
                    {
                        //response = await _accountService.TransferAsync(id, transactionDTO);
                        response = "";
                    }
                    else if (transactionDTO.Type == "Deposito")
                    {
                        response = await _accountService.DepositAsync(id, transactionDTO);
                    }
                    else
                    {
                        return BadRequest("Type of transaction doesn't exist");
                    }

                    return Ok(response);
                }

                return BadRequest("Account doesn't belong to user.");
            }
            return BadRequest("Amount must be greater than 0,01");
        }
    }
}
