using Stock_Manager_Simulator_Backend.Enums;
using Stock_Manager_Simulator_Backend.Models;

namespace Stock_Manager_Simulator_Backend.Repositories.Interfaces
{
    public interface IRankRepository
    {
        Task CreateRankAsync(Rank rank);
        Task<List<Rank>> GetLatestUsersByTypeAsync(RankType rankType);
        Task SaveChangesAsync();
        Task<Rank?> GetLatestRankByUserAndTypeAsync (int userId, RankType rankType);
        Task<List<Rank>> GetLatestRanksAsync();
    }
}