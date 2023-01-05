using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Core.Services;
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

        //[HttpGet]
        //[Authorize("Regular")]
        //public async Task<IActionResult> Get()
        //{
        //    return Ok(await _transactionService.GetAllAsync());
        //}

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Get([FromQuery] int? page = 1)
        {
            try
            {
                PagedList<TransactionPagedDTO> pageTransaction = _transactionService.GetAllPage(page.Value);

                if (page > pageTransaction.TotalPages)
                    return BadRequest($"page number {page} doesn't exist");
                else
                {
                    var url = this.Request.Path;
                    return Ok(new
                    {
                        next = pageTransaction.HasNext ? $"{url}/{page + 1}" : "",
                        prev = (pageTransaction.Count > 0 && pageTransaction.HasPrevious) ? $"{url}/{page - 1}" : "",
                        currentPage = pageTransaction.CurrentPage,
                        totalPages = pageTransaction.TotalPages,
                        data = pageTransaction
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = int.Parse(User.FindFirst("UserId").Value);
            var transaction = await _transactionService.GetById(id, userId);
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