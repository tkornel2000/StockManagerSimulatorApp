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

        public Task<User?> GetUserByIdAsync(int id)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task SavaChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            user.IsDelete = true;
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
