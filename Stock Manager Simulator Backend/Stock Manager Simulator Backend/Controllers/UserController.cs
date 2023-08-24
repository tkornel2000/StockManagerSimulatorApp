using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return null;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
    }
}
