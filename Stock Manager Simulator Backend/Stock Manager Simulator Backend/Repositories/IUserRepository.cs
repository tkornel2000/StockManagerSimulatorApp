using Stock_Manager_Simulator_Backend.Models;

namespace Stock_Manager_Simulator_Backend.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByEmailAsync(string email);
        Task CreateUserAsync(User user);
        Task SavaChangesAsync();
        Task DeleteUserAsync(User user);
        bool WithThisEmailThereIsNoUser(string email);
        bool WithThisUsernameThereIsNoUser(string username);
    }
}