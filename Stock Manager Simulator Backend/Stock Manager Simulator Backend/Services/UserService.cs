using AutoMapper;
using Stock_Manager_Simulator_Backend.Constans;
using Stock_Manager_Simulator_Backend.Dtos;
using Stock_Manager_Simulator_Backend.Dtos.Results;
using Stock_Manager_Simulator_Backend.Models;
using Stock_Manager_Simulator_Backend.Repositories.Interfaces;
using Stock_Manager_Simulator_Backend.Services.Interfaces;
using System.Security.Cryptography;

namespace Stock_Manager_Simulator_Backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, IMapper mapper, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task CreateUserAsync(RegisterDto registerDto)
        {
            var user = _mapper.Map<User>(registerDto);
            user.PasswordHash = HashPassword(registerDto.Password);
            await _userRepository.CreateUserAsync(user);
        }

        public async Task<string> ChangeUserPassword(int id, ChangePasswordDto changePasswordDto)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return ErrorConstans.THERE_IS_NO_USER_WITH_THIS_ID;
            }

            if (!ValidatePassword(changePasswordDto.OldPassword, user.PasswordHash))
            {
                return ErrorConstans.YOUR_OLD_PASSWORD_WAS_WRONG;
            }

            user.PasswordHash = HashPassword(changePasswordDto.NewPassword);
            await _userRepository.SaveChangesAsync();
            return "";
        }

        public async Task<string> DeleteUserAsync(int id, ConfirmDeleteDto confirmDeleteDto)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return ErrorConstans.THERE_IS_NO_USER_WITH_THIS_ID;
            }

            if (!ValidatePassword(confirmDeleteDto.ConfirmPassword,user.PasswordHash))
            {
                return ErrorConstans.YOU_CAN_NOT_DELETE_THIS_ACCOUNT_BECAUSE_PASSWORD_IS_INVALID;
            }

            await _userRepository.DeleteUserAsync(user);
            return "";
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public bool ValidatePassword(string inputPassword, string passwordHash)
        {
            string hashedInputPassword = HashPassword(inputPassword);
            return string.Equals(hashedInputPassword, passwordHash/*, StringComparison.OrdinalIgnoreCase*/);
        }

        public async Task<UserDto?> GetMySelfAsync()
        {
            var user = await _userRepository.GetUserByIdAsync(_tokenService.GetMyId());
            if (user == null)
            {
                return null;
            }

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<PutUserResult> PutUserAsync(int id, PutUserDto putUserDto)
        {
            var oldUser = await _userRepository.GetUserByIdAsync(id);
            if (oldUser?.Username != putUserDto.Username)
            {
                if (!_userRepository.WithThisUsernameThereIsNoUser(putUserDto.Username))
                {
                    return new PutUserResult { ErrorMessage= ErrorConstans.THERE_IS_USER_WITH_THIS_USERNAME };
                };
            }
            
            if (oldUser?.Email != putUserDto.Email)
            {
                if (!_userRepository.WithThisEmailThereIsNoUser(putUserDto.Email))
                {
                    return new PutUserResult { ErrorMessage= ErrorConstans.THERE_IS_USER_WITH_THIS_EMAIL };
                };
            }

            _mapper.Map(putUserDto, oldUser);
            await _userRepository.SaveChangesAsync();
            return new PutUserResult { PutUserDto=putUserDto};
        }

    }
}
