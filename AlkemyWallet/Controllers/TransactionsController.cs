using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AlkemyWallet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        [Authorize("Regular")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _transactionService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [Authorize("Regular")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = int.Parse(User.Claims.First(x => x.Type == "UserId").Value);
            var transaction = await _transactionService.GetById(id, user);
            if (transaction is null)
                return NoContent();
            else
                return Ok(transaction);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task Delete(int id)
        {
            await _transactionService.Delete(id);
         }

        [HttpPut("{id}")]
        [Authorize(Roles="Admin")]
        public async Task Put(int id, TransactionDetailsDTO transactionDTO)
        {
            await _transactionService.UpdateAsync(id, transactionDTO);
        }


        [HttpPost]
        public async Task<IActionResult> Post(TransactionDTO transactionDTO)
        {
            var result = await _transactionService.CreateTransactionAsync(transactionDTO);

            if (result != null)
            {
                return Ok(result);
            }
            
            return BadRequest();
        }
    }
}