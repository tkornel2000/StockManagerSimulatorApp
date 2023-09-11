using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Stock_Manager_Simulator_Backend.Constans;
using Stock_Manager_Simulator_Backend.Repositories.Interfaces;

public class UpdatePortfolioService : BackgroundService
{
    private readonly ILogger<UpdatePortfolioService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public UpdatePortfolioService(ILogger<UpdatePortfolioService> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("UpdatePortfolioService is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                var users = await _userRepository.GetAllAsync();
                foreach (var user in users)
                {
                    var value = await _userRepository.GetCurrentStockValueByUserAsync(user.Id);
                    user.StockValue = value;
                    await _userRepository.SaveChangesAsync();
                }
            }

            // Várakozás 10 mpig
            await Task.Delay(TimeSpan.FromMinutes(5));
        }
    }
}
