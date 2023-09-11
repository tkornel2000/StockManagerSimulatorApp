using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stock_Manager_Simulator_Backend.Dtos;
using Stock_Manager_Simulator_Backend.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stock_Manager_Simulator_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET: api/<TransactionController>
        [HttpGet]
        public async Task<ActionResult<List<TransactionDto>>> GetAllMyTransaction()
        {
            var result = await _transactionService.GetAllMyTransactionAsync();
            return Ok(result);
        }

        //TODO vissza kell adni egy olyan listát ahol részletesebb adatok vannak a részvényről(név jelenlegi ár)
        [HttpGet("available-stocks")]
        public async Task<ActionResult<List<StockQuantityWithStockDto>>> GetAllMyAvailablyStock()
        {
            var result = await _transactionService.GetAllMyAvailableStockQuantityAsync();
            return Ok(result);
        }

        // POST api/<TransactionController>
        [HttpPost("buy")]
        public async Task<ActionResult> PostBuyTransactionAsync([FromBody] StockQuantityDto stockQuantityDto)
        {
            var result = await _transactionService.CreateBuyTransactionAsync(stockQuantityDto);
            if (result == "")
            {
                return NoContent();
            }
            return BadRequest(new {error = result});
        }

        // POST api/<TransactionController>
        [HttpPost("sell")]
        public async Task<ActionResult> PostSellTransactionAsync([FromBody] StockQuantityDto stockQuantityDto)
        {
            var result = await _transactionService.CreateSellTransactionAsync(stockQuantityDto);
            if (result == "")
            {
                return NoContent();
            }
            return BadRequest(new { error = result });
        }
    }
}
