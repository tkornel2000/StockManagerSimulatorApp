using Microsoft.AspNetCore.Mvc;
using Stock_Manager_Simulator_Backend.Dtos;
using Stock_Manager_Simulator_Backend.Models;
using Stock_Manager_Simulator_Backend.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stock_Manager_Simulator_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Test : ControllerBase
    {
        private readonly IStockRepository _stockRepository;

        public Test(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        // GET: api/<Test>
        [HttpGet]
        public async Task<IEnumerable<int>> GetAsync()
        {
            await _stockRepository.GetAllStockLastPriceAsync();
            var allStockLastPrice = new List<int>();
            return allStockLastPrice;
        }
    }
}
