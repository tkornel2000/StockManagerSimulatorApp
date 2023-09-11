using Microsoft.AspNetCore.Mvc;
using Secret_Sharing_Platform.Dto;
using Stock_Manager_Simulator_Backend.Constans;
using Stock_Manager_Simulator_Backend.Dtos;
using Stock_Manager_Simulator_Backend.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stock_Manager_Simulator_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AuthController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenDto>> Post([FromBody] LoginDto loginDto)
        {
            var userResult = await _accountService.LoginAsync(loginDto);
            if (userResult.Error is not null)
            {
                return Unauthorized(new { error = userResult.Error });
            }
            if (userResult.User is null)
            {
                return BadRequest(ErrorConstans.THERE_IS_AN_UNEXPECTED_ERROR);
            }
            var tokenDto = _tokenService.CreateToken(userResult.User);
            return Ok(tokenDto);
        }
    }
}
