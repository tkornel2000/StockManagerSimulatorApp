using Stock_Manager_Simulator_Backend.Constans;
using Stock_Manager_Simulator_Backend.Dtos;
using Stock_Manager_Simulator_Backend.Dtos.Results;
using Stock_Manager_Simulator_Backend.Models;
using Stock_Manager_Simulator_Backend.Repositories;

namespace Stock_Manager_Simulator_Backend.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public AccountService(IUserRepository userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task<UserResult> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(loginDto.Username);
            if (user is null)
            {
                return new UserResult { Error = ErrorConstans.THERE_IS_NO_USER_WITH_THIS_USERNAME };
            }

            if (!_userService.ValidatePassword(loginDto.Password, user.PasswordHash))
            {
                return new UserResult { Error = ErrorConstans.WRONG_PASSWORD };
            }
            return new UserResult { User = user };
        }
    }
}
