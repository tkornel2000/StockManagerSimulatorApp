﻿using AutoMapper;
using Stock_Manager_Simulator_Backend.Constans;
using Stock_Manager_Simulator_Backend.Dtos;
using Stock_Manager_Simulator_Backend.Models;
using Stock_Manager_Simulator_Backend.Repositories;
using System.Security.Cryptography;

namespace Stock_Manager_Simulator_Backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
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
            await _userRepository.SavaChangesAsync();
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


    }
}
