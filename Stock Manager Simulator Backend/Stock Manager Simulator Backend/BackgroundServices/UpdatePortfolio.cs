using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Stock_Manager_Simulator_Backend.Repositories;
using Stock_Manager_Simulator_Backend.Constans;

public class UpdatePortfolio : BackgroundService
{
    private readonly ILogger<UpdatePortfolio> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public UpdatePortfolio(ILogger<UpdatePortfolio> logger, IServiceScopeFactory serviceScopeFactory)
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
                var _transactionRepository = scope.ServiceProvider
                    .GetRequiredService<ITransactionRepository>();
                var users = await _userRepository.GetAllAsync();
                foreach (var user in users)
                {
                    var value = await _transactionRepository.GetCurrentStockValueByUser(user.Id);
                    user.StockValue = value;
                    await _userRepository.SaveChangesAsync();
                    _logger.LogError($"update @{user.Email}");
                }
            }

            // Várakozás 10 mpig
            await Task.Delay(TimeSpan.FromSeconds(10));
        }

        _logger.LogInformation("UpdatePortfolioService is stopping.");
    }
}
