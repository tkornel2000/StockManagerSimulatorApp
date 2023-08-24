using Stock_Manager_Simulator_Backend.Dtos;
using Stock_Manager_Simulator_Backend.Dtos.Results;

namespace Stock_Manager_Simulator_Backend.Services
{
    public interface IAccountService
    {
        Task<UserResult> LoginAsync(LoginDto loginDto);
    }
}