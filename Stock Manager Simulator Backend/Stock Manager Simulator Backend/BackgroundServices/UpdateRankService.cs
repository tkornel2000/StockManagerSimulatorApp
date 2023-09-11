using Stock_Manager_Simulator_Backend.Repositories.Interfaces;
using Stock_Manager_Simulator_Backend.Services.Interfaces;

namespace Stock_Manager_Simulator_Backend.BackgroundServices
{
    public class UpdateRankService : BackgroundService
    {
        private readonly ILogger<UpdatePortfolioService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UpdateRankService(ILogger<UpdatePortfolioService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("UpdateRankService is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _rankService = scope.ServiceProvider.GetRequiredService<IRankService>();
                    _logger.LogInformation("start updateRank");
                    await _rankService.UpdateRankAsync();
                    _logger.LogInformation("end updateRank");
                }

                // Várakozás 10 mpig
                await Task.Delay(TimeSpan.FromSeconds(20));
            }
        }
    }
}
