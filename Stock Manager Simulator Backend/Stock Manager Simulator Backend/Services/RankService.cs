using AutoMapper;
using Stock_Manager_Simulator_Backend.Enums;
using Stock_Manager_Simulator_Backend.Models;
using Stock_Manager_Simulator_Backend.Repositories.Interfaces;
using Stock_Manager_Simulator_Backend.Services.Interfaces;
using System.Globalization;

namespace Stock_Manager_Simulator_Backend.Services
{
    public class RankService : IRankService
    {
        private readonly IRankRepository _rankRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RankService> _logger;


        public RankService(IRankRepository rankRepository, IUserRepository userRepository, IMapper mapper, ILogger<RankService> logger)
        {
            _rankRepository = rankRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<RankDto>> GetLatestRankByTypeAsync(RankType rankType)
        {
            var ranks = await _rankRepository.GetLatestUsersByTypeAsync(rankType);
            var rankDtos = ranks.Select(rank => _mapper.Map<RankDto>(rank));
            return rankDtos;
        }

        public async Task<IEnumerable<RankDto>> GetLatestRankAsync()
        {
            var ranks = await _rankRepository.GetLatestRanksAsync();
            var rankDtos = ranks.Select(rank => _mapper.Map<RankDto>(rank));
            return rankDtos;
        }

        public async Task UpdateRankAsync()
        {
            Calendar cal = new CultureInfo("en-US").Calendar;
            var today = DateTime.Today;
            var users = await _userRepository.GetAllAsync();
            foreach (var user in users)
            {
                var currentValue = await _userRepository.GetCurrentPortfolioValueByUserAsync(user.Id);
                var latestDailyRank = await _rankRepository
                    .GetLatestRankByUserAndTypeAsync(user.Id, RankType.Daily);
                if (latestDailyRank == null || latestDailyRank.Datetime.Day != today.Day)
                {
                    // Ha ide belép akkor hozzá kell adni a rank-ot

                    var dailyRank = new Rank
                    {
                        CurrentValue = currentValue,
                        Datetime = today,
                        PreviousValue = latestDailyRank == null ? 2000000 : latestDailyRank.CurrentValue,
                        RankType = RankType.Daily,
                        UserId = user.Id,
                        User = user
                    };
                    await _rankRepository.CreateRankAsync(dailyRank);

                    var latestWeeklyRank = await _rankRepository.GetLatestRankByUserAndTypeAsync(user.Id, RankType.Weekly);
                    if (latestWeeklyRank == null ||
                        cal.GetWeekOfYear(latestWeeklyRank.Datetime, CalendarWeekRule.FirstDay, DayOfWeek.Monday)
                        != cal.GetWeekOfYear(today, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                    {
                        var weeklyRank = new Rank
                        {
                            CurrentValue = currentValue,
                            Datetime = today,
                            PreviousValue = latestWeeklyRank == null ? 2000000 : latestWeeklyRank.CurrentValue,
                            RankType = RankType.Weekly,
                            UserId = user.Id
                        };
                        await _rankRepository.CreateRankAsync(weeklyRank);
                    }

                    var latestMonthlyRank = await _rankRepository.GetLatestRankByUserAndTypeAsync(user.Id, RankType.Monthly);
                    if (latestMonthlyRank == null || latestMonthlyRank.Datetime.Month != today.Month)
                    {
                        _logger.LogWarning("beléptem");
                        _logger.LogError(latestMonthlyRank?.ToString());
                        var monthlyRank = new Rank
                        {
                            CurrentValue = currentValue,
                            Datetime = today,
                            PreviousValue = latestMonthlyRank == null ? 2000000 : latestMonthlyRank.CurrentValue,
                            RankType = RankType.Monthly,
                            UserId = user.Id
                        };
                        await _rankRepository.CreateRankAsync(monthlyRank);
                    }

                    var latestAllTimeRank = await _rankRepository.GetLatestRankByUserAndTypeAsync(user.Id, RankType.AllTime);
                    if (latestAllTimeRank == null)
                    {
                        var allTimeRank = new Rank
                        {
                            CurrentValue = currentValue,
                            Datetime = today,
                            PreviousValue = latestAllTimeRank == null ? 2000000 : latestAllTimeRank.CurrentValue,
                            RankType = RankType.AllTime,
                            UserId = user.Id
                        };
                        await _rankRepository.CreateRankAsync(allTimeRank);
                    }
                }
                else
                {
                    //Frissíteni kell.
                    var portfolioValue = await _userRepository.GetCurrentPortfolioValueByUserAsync(user.Id);
                    latestDailyRank.CurrentValue = portfolioValue;
                    //await _rankRepository.SaveChangesAsync();

                    var latestWeeklyRank = await _rankRepository.GetLatestRankByUserAndTypeAsync(user.Id, RankType.Weekly);
                    latestWeeklyRank!.CurrentValue = currentValue;

                    var latestMonthlyRank = await _rankRepository.GetLatestRankByUserAndTypeAsync(user.Id, RankType.Monthly);
                    latestMonthlyRank!.CurrentValue = currentValue;

                    var allTimeRank = await _rankRepository.GetLatestRankByUserAndTypeAsync(user.Id, RankType.AllTime);
                    allTimeRank!.CurrentValue = currentValue;
                }
            }
            await _rankRepository.SaveChangesAsync();
        }
    }
}
