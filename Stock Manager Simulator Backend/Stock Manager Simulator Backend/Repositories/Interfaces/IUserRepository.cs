using Stock_Manager_Simulator_Backend.Models;

namespace Stock_Manager_Simulator_Backend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByEmailAsync(string email);
        Task CreateUserAsync(User user);
        Task SaveChangesAsync();
        Task DeleteUserAsync(User user);
        //Task HandleStockBuyForUserAsync(User user, float buyValue);
        Task HandleStockBuyForUserAsync(int id, float buyValue);
        Task HandleStockSellForUserAsync(int id, float sellValue);
        bool WithThisEmailThereIsNoUser(string email);
        bool WithThisUsernameThereIsNoUser(string username);
        Task<float> GetCurrentPortfolioValueByUserAsync(int userId);
        Task<float> GetCurrentStockValueByUserAsync(int userId);
    }
}