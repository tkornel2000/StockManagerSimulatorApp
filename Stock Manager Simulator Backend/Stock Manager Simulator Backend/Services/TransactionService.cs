using AutoMapper;
using Stock_Manager_Simulator_Backend.Constans;
using Stock_Manager_Simulator_Backend.Dtos;
using Stock_Manager_Simulator_Backend.Models;
using Stock_Manager_Simulator_Backend.Repositories.Interfaces;
using Stock_Manager_Simulator_Backend.Services.Interfaces;

namespace Stock_Manager_Simulator_Backend.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IStockRepository _stockRepository;
        private readonly IUserRepository _userRepository;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper, IUserService userService, IUserRepository userRepository, IStockRepository stockRepository)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _userService = userService;
            _userRepository = userRepository;
            _stockRepository = stockRepository;
        }

        public async Task<string> CreateBuyTransactionAsync(StockQuantityDto stockQuantityDto)
        {
            if (!StockConstans.AllStockSymbol.Contains(stockQuantityDto.StockSymbol))
            {
                return ErrorConstans.THERE_IS_NO_STOCK_WITH_THIS_SYMBOL;
            }

            if (stockQuantityDto.Quantity<=0)
            {
                return ErrorConstans.YOU_CAN_NOT_BUY_ZERO_OR_MINUS_NUMBER_OF_STOCK;
            }

            var actStockPrice = (await _stockRepository
                .GetSpecificStockLastPriceAsync(stockQuantityDto.StockSymbol)).Price;
            var buyValue = actStockPrice * stockQuantityDto.Quantity;

            var meDto = await _userService.GetMySelfAsync();

            if (meDto == null)
            {
                return ErrorConstans.THERE_IS_NO_USER_WITH_THIS_ID;
            }

            if (meDto.Money < buyValue)
            {
                return ErrorConstans.YOU_DO_NOT_HAVE_ENOUGH_MONEY;
            }

            var transaction = _mapper.Map<Transaction>(stockQuantityDto);
            transaction.TimeInTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            transaction.Price = actStockPrice;
            transaction.UserId = meDto.Id;

            await _transactionRepository.CreateTransactionAsync(transaction);

            await _userRepository
                .HandleStockBuyForUserAsync(meDto.Id, buyValue);

            return "";
        }

        public async Task<List<TransactionDto>> GetAllMyTransactionAsync()
        {
            var meDto = await _userService.GetMySelfAsync();
            if (meDto == null)
            {
                return new List<TransactionDto>();
            }

            var transactionList = await _transactionRepository.GetAllTransactionByUserAsync(meDto.Id);
            var transactionDtoList =  transactionList.Select(x => _mapper.Map<TransactionDto>(x)).ToList();
            return transactionDtoList;
        }

        public async Task<List<StockQuantityDto>> GetAllMyAvailableStockQuantityAsync()
        {
            var meDto = await _userService.GetMySelfAsync();
            if (meDto == null)
            {
                return new List<StockQuantityDto>();
            }

            var stockQuantityDtoList = await _transactionRepository
                .GetAllAvailableStockQuantityByUserAsync(meDto.Id);
            stockQuantityDtoList = stockQuantityDtoList.Where(x => x.Quantity != 0).ToList();
            return stockQuantityDtoList;
        }

        public async Task<string> CreateSellTransactionAsync(StockQuantityDto stockQuantityDto)
        {
            if (!StockConstans.AllStockSymbol.Contains(stockQuantityDto.StockSymbol))
            {
                return ErrorConstans.THERE_IS_NO_STOCK_WITH_THIS_SYMBOL;
            }

            if (stockQuantityDto.Quantity <= 0)
            {
                return ErrorConstans.YOU_CAN_NOT_SELL_ZERO_OR_MINUS_NUMBER_OF_STOCK;
            }
            var actStockPrice = (await _stockRepository
                .GetSpecificStockLastPriceAsync(stockQuantityDto.StockSymbol)).Price;
            var sellValue = actStockPrice * stockQuantityDto.Quantity;

            var meDto = await _userService.GetMySelfAsync();
            if (meDto == null)
            {
                return ErrorConstans.THERE_IS_NO_USER_WITH_THIS_ID;
            }

            var availableStockQuantityForThisSymbol = (await _transactionRepository
                .GetAvailableStockQuantityByUserAndSymbolAsync(meDto.Id, stockQuantityDto.StockSymbol))
                .Quantity;
            if (availableStockQuantityForThisSymbol < stockQuantityDto.Quantity)
            {
                return ErrorConstans.YOU_CAN_NOT_SELL_THAT_MANY_STOCK_BECOUSE_YOU_DO_NOT_HAVE_THAT_MANY;
            }

            await _userRepository.HandleStockSellForUserAsync(meDto.Id, sellValue);
            var transaction = _mapper.Map<Transaction>(stockQuantityDto);
            transaction.TimeInTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            transaction.Price = actStockPrice;
            transaction.UserId = meDto.Id;
            transaction.Quantity = -transaction.Quantity;

            await _transactionRepository.CreateTransactionAsync(transaction);

            return "";
        }
    }
}
