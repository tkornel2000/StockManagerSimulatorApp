using Stock_Manager_Simulator_Backend.Dtos;

namespace Stock_Manager_Simulator_Backend.Services.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(RegisterDto userDto);
        Task<string> ChangeUserPassword(int id, ChangePasswordDto changePasswordDto);
        Task<string> DeleteUserAsync(int id, ConfirmDeleteDto confirmDeleteDto);
        string HashPassword(string password);
        bool ValidatePassword(string inputPassword, string passwordHash);
        Task<UserDto?> GetMySelfAsync();
    }
}