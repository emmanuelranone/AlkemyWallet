using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client;

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

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, AccountUpdateDTO accountDTO)
        {
            var existAccount = await _accountService.GetByIdUpdateAsync(id);
            if (existAccount == null)
            {
                return NotFound();
            }
            var result = await _accountService.UpdateAsync(id, accountDTO);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync(int id, [FromBody] JsonPatchDocument<AccountUpdateDTO> patchDoc)
        {
            if (id == 0 || patchDoc == null)
            {
                return BadRequest();
            }

            var existAccount = await _accountService.GetByIdUpdateAsync(id);
            if (existAccount == null)
            {
                return NotFound();
            }

            var updatedAccount = await _accountService.UpdatePatchAsync(existAccount, patchDoc);
            if (updatedAccount == null)
            {
                return NotFound();
            }
            return Ok(updatedAccount);
        }

        //// DELETE api/<AccountController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
