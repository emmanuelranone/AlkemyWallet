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

        /// <summary>
        ///    Get. Transaction list page. Only available for Administrator.
        /// </summary>
        /// <param>Get All</param>
        /// <remarks>
        ///     Sample request: api/transactions
        /// </remarks>
        /// <returns> Get all registered Transaction</returns>
        /// <response code="200">All transactions were obtained</response>
        /// <response code="401">The JWT access token has not been indicated or is incorrect.</response>
        /// <response code="403">User is not authorized because isn't a administrator.</response>
        /// <response code="404">Request Failed.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<TransactionPagedDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet]
        [Authorize("Admin")]
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


        /// <summary>
        ///    Get transaction by id. Only available for Regulars User.
        /// </summary>
        /// <param>Get ID</param>
        /// <remarks>
        ///     Sample request: api/transactions/{id}
        /// </remarks>
        /// <returns> Get by Id registered Transaction</returns>
        /// <response code="200">The transaction was obtained</response>
        /// <response code="401">The JWT access token has not been indicated or is incorrect.</response>
        /// <response code="403">User is not authorized because isn't a regular user.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Transaction))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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

        /// <summary>
        ///    Delete transaction by id. Only available for Administrators.
        /// </summary>
        /// <param>Delete ID</param>
        /// <remarks>
        ///     Sample request: api/transactions/{id}
        /// </remarks>
        /// <returns>Deleted transaction</returns>
        /// <response code="200">The transaction was deleted</response>
        /// <response code="401">The JWT access token has not been indicated or is incorrect.</response>
        /// <response code="403">User is not authorized because isn't an administrator.</response>
        /// <response code="404">The object was not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Transaction))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedTransaction = await _transactionService.Delete(id);
            if (deletedTransaction > 0)
                return Ok();
            else
                return NotFound();
         }

        /// <summary>
        ///    Update transaction by id. Only available for Administrators.
        /// </summary>
        /// <remarks>
        ///     Sample request: api/transactions/{id}
        /// </remarks>
        /// <returns>Updated transaction</returns>
        /// <response code="200">The transaction was updated</response>
        /// <response code="400">Not found</response>
        /// <response code="401">The JWT access token has not been indicated or is incorrect.</response>
        /// <response code="403">User is not authorized because isn't an administrator.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Transaction))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("{id}")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Put(int id, TransactionDetailsDTO transactionDTO)
        {
           var updatedTransaction = await _transactionService.UpdateAsync(id, transactionDTO);
           if (updatedTransaction == null)
                return NotFound();
            
            return Ok(updatedTransaction);
        }

        /// <summary>
        ///    Inserted transaction in the table.
        /// </summary>
        /// <remarks>
        ///     Sample request: api/transactions
        /// </remarks>
        /// <returns>Inserted transaction</returns>
        /// <response code="200">The transaction was inserted</response>
        /// <response code="400">.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Transaction))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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