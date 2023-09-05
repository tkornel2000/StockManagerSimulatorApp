using Microsoft.AspNetCore.Mvc;
using Stock_Manager_Simulator_Backend.Constans;
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
        private readonly ITransactionRepository _transactionRepository;

        public Test(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        // GET: api/<Test>
        [HttpGet]
        public async Task<ActionResult<float>> Get()
        {
            var result = await _transactionRepository.GetCurrentStockValueByUser(54);
            return Ok(result);
        }
    }
}
