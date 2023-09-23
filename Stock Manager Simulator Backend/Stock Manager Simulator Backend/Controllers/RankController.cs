using Microsoft.AspNetCore.Mvc;
using Stock_Manager_Simulator_Backend.Enums;
using Stock_Manager_Simulator_Backend.Models;
using Stock_Manager_Simulator_Backend.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stock_Manager_Simulator_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankController : ControllerBase
    {
        private readonly IRankService _rankService;

        public RankController(IRankService rankService)
        {
            _rankService = rankService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RankDto>>> GetAllLatestRanks()
        {
            var result = await _rankService.GetLatestRankAsync();
            return Ok(result);
        }

        // GET: api/<RankController>
        [HttpGet("{rankType}")]
        public async Task<ActionResult<IEnumerable<RankDto>>> GetDailyRank(string rankType)
        {
            RankType enumRankType;

            switch (rankType.ToLower()) // Konvertáljuk kisbetűsre a bejövő szöveget
            {
                case "daily":
                    enumRankType = RankType.Daily;
                    break;
                case "weekly":
                    enumRankType = RankType.Weekly;
                    break;
                case "monthly":
                    enumRankType = RankType.Monthly;
                    break;
                case "alltime":
                    enumRankType = RankType.AllTime;
                    break;
                default:
                    // Érvénytelen érték esetén BadRequest választ küldünk
                    return BadRequest("Érvénytelen rang típus.");
            }

            var result = await _rankService.GetLatestRankByTypeAsync(enumRankType);
            return Ok(result);
        }

        // GET api/<RankController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<RankController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RankController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RankController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
