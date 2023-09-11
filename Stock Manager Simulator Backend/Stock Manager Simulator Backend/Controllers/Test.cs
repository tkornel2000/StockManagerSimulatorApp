using Microsoft.AspNetCore.Mvc;
using Stock_Manager_Simulator_Backend.Constans;
using Stock_Manager_Simulator_Backend.Dtos;
using Stock_Manager_Simulator_Backend.Enums;
using Stock_Manager_Simulator_Backend.Models;
using Stock_Manager_Simulator_Backend.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stock_Manager_Simulator_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Test : ControllerBase
    {
        private readonly IRankRepository _rankRepository;

        public Test(IRankRepository rankRepository)
        {
            _rankRepository = rankRepository;
        }

        // GET: api/<Test>
        [HttpGet]
        public async Task<ActionResult<RankType>> Get()
        {
            return Ok(await _rankRepository.GetLatestUsersByTypeAsync(Enums.RankType.Weekly));
        }

        [HttpPost]
        public async Task<ActionResult<RankType>> Post()
        {
            var dailyRank = new Rank
            {
                CurrentValue = 110000,
                Datetime = DateTime.Today,
                PreviousValue = 100000,
                RankType = RankType.Daily,
                UserId = 54,
            };
            await _rankRepository.CreateRankAsync(dailyRank);
            return Ok();
        }
    }
}
