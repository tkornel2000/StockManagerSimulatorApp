using Microsoft.EntityFrameworkCore;
using Stock_Manager_Simulator_Backend.Data;
using Stock_Manager_Simulator_Backend.Enums;
using Stock_Manager_Simulator_Backend.Models;
using Stock_Manager_Simulator_Backend.Repositories.Interfaces;

namespace Stock_Manager_Simulator_Backend.Repositories
{
    public class RankRepository : IRankRepository
    {
        private readonly ApplicationDbContext _context;

        public RankRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Rank>> GetLatestUsersByTypeAsync(RankType rankType)
        {
            var ranks =  _context.Ranks.Where(x => x.RankType == rankType);
            if (ranks.Any())
            {
                var ranksAfterObGbF = ranks
                .Include(x => x.User)
                .GroupBy(x => x.UserId)
                .Select(y => new Rank
                {
                    Id = y.OrderByDescending(x => x.Datetime).Select(x => x.Id).FirstOrDefault(),
                    UserId = y.OrderByDescending(x => x.Datetime).Select(x => x.UserId).FirstOrDefault(),
                    CurrentValue = y.OrderByDescending(x => x.Datetime).Select(x => x.CurrentValue).FirstOrDefault(),
                    Datetime = y.OrderByDescending(x => x.Datetime).Select(x => x.Datetime).FirstOrDefault(),
                    PreviousValue = y.OrderByDescending(x => x.Datetime).Select(x => x.PreviousValue).FirstOrDefault(),
                    RankType = rankType,
                    User = y.OrderByDescending(x => x.Datetime).FirstOrDefault()!.User,
                });
                return await ranksAfterObGbF.ToListAsync();
            }
            return new List<Rank>();
        }

        public Task<List<Rank>> GetLatestRanksAsync()
        {
            return _context.Ranks
                .Include(x => x.User)
                .GroupBy(x => x.RankType)
                .Select(y => new Rank
                {
                    Id = y.OrderByDescending(x => x.Datetime).Select(x => x.Id).FirstOrDefault(),
                    UserId = y.OrderByDescending(x => x.Datetime).Select(x => x.UserId).FirstOrDefault(),
                    CurrentValue = y.OrderByDescending(x => x.Datetime).Select(x => x.CurrentValue).FirstOrDefault(),
                    Datetime = y.OrderByDescending(x => x.Datetime).Select(x => x.Datetime).FirstOrDefault(),
                    PreviousValue = y.OrderByDescending(x => x.Datetime).Select(x => x.PreviousValue).FirstOrDefault(),
                    RankType = y.OrderByDescending(x => x.Datetime).Select(x => x.RankType).FirstOrDefault(),
                    User = y.OrderByDescending(x => x.Datetime).FirstOrDefault()!.User,
                }).ToListAsync();
        }


        public async Task CreateRankAsync(Rank rank)
        {
            _context.Ranks.Add(rank);
            await _context.SaveChangesAsync();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<Rank?> GetLatestRankByUserAndTypeAsync(int userId, RankType rankType)
        {
            var ranks = _context.Ranks;
            if (ranks.Any())
            {
                return await ranks
                .Where(x => x.UserId == userId)
                .Where(x => x.RankType == rankType)
                .OrderByDescending(x => x.Datetime)
                .FirstOrDefaultAsync();
            }
            return null;
        }
    }
}
