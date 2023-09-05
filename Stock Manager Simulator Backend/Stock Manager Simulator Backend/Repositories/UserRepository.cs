using Microsoft.EntityFrameworkCore;
using Stock_Manager_Simulator_Backend.Data;
using Stock_Manager_Simulator_Backend.Models;

namespace Stock_Manager_Simulator_Backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<User>> GetAllAsync()
        {
            return _context.Users.Where(x => x.IsDelete==false).ToListAsync();
        }

        public Task<User?> GetUserByIdAsync(int id)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<User?> GetUserByUsernameAsync(string username)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public Task<User?> GetUserByEmailAsync(string email)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            user.IsDelete = true;
            await _context.SaveChangesAsync();
        }


        public async Task HandleStockBuyForUserAsync(int id, float buyValue)
        {
            var user = _context.Users.First(x => x.Id == id);
            user.Money -= buyValue;
            user.StockValue += buyValue;
            await _context.SaveChangesAsync();
        }

        public async Task HandleStockSellForUserAsync(int id, float sellValue)
        {
            var user = _context.Users.First(x => x.Id == id);
            user.Money += sellValue;
            user.StockValue -= sellValue;
            await _context.SaveChangesAsync();
        }

        public bool WithThisEmailThereIsNoUser(string email)
        {
            var result = _context.Users.FirstOrDefault(x => x.Email == email);
            return result == null;
        }
        
        public bool WithThisUsernameThereIsNoUser(string username)
        {
            var result = _context.Users.FirstOrDefault(x => x.Username == username);
            return result == null;
        }
    }
}
