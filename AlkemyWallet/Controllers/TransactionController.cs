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
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
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

    }
}