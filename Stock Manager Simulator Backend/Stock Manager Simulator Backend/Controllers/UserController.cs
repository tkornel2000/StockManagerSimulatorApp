using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stock_Manager_Simulator_Backend.Constans;
using Stock_Manager_Simulator_Backend.Dtos;
using Stock_Manager_Simulator_Backend.Services;
using Stock_Manager_Simulator_Backend.Validators;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stock_Manager_Simulator_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IValidator<ChangePasswordDto> _changePasswordValidator;

        public UserController(IUserService userService, IValidator<RegisterDto> registerValidator, IValidator<ChangePasswordDto> changePasswordValidator)
        {
            _userService = userService;
            _registerValidator = registerValidator;
            _changePasswordValidator = changePasswordValidator;
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RegisterDto registerDto)
        {
            var result = await _registerValidator.ValidateAsync(registerDto);
            if (!result.IsValid)
            {
                return BadRequest(new { Error = result.Errors.First().ErrorMessage });
            }
            await _userService.CreateUserAsync(registerDto);
            return NoContent();
        }

        // PUT api/<UserController>/5
        [Authorize]
        [HttpPut("change-password/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ChangePasswordDto changePasswordDto)
        {
            var validateResult = await _changePasswordValidator.ValidateAsync(changePasswordDto);
            if (!validateResult.IsValid)
            {
                return BadRequest(new { Error = validateResult.Errors.First().ErrorMessage });
            }

            var result = await _userService.ChangeUserPassword(id, changePasswordDto);
            if (result != "")
            {
                return BadRequest(new { Error = result });
            }

            return NoContent();
        }

        // DELETE api/<UserController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, ConfirmDeleteDto confirmDeleteDto)
        {
            var result = await _userService.DeleteUserAsync(id, confirmDeleteDto);
            if (result == "")
            {
                return NoContent();
            }

            return BadRequest(new { Error = result });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserDto>> GetMySelfAsync()
        {
            var result = await _userService.GetMySelfAsync();
            if (result == null)
            {
                return BadRequest(new { Error = ErrorConstans.THERE_IS_AN_UNEXPECTED_ERROR });
            }

            return Ok(result);
        }
    }
}
