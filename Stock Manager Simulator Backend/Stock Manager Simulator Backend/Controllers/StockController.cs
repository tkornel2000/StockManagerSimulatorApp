using Microsoft.AspNetCore.Mvc;
using Stock_Manager_Simulator_Backend.Dtos;
using Stock_Manager_Simulator_Backend.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stock_Manager_Simulator_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        // GET: api/<StockController>
        [HttpGet]
        public async Task<ActionResult<List<StockPriceDto>>> GetAsync()
        {
            var allStockPriceDtos = await _stockService.GetAllStockLastPriceAsync();
            return Ok(allStockPriceDtos);
        }

        // GET api/<StockController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
