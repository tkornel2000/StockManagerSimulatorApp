using Stock_Manager_Simulator_Backend.Enums;
using Stock_Manager_Simulator_Backend.Models;

namespace Stock_Manager_Simulator_Backend.Services.Interfaces
{
    public interface IRankService
    {
        Task<IEnumerable<RankDto>> GetLatestRankByTypeAsync(RankType rankType);
        Task<IEnumerable<RankDto>> GetLatestRankAsync();
        Task UpdateRankAsync();
    }
}