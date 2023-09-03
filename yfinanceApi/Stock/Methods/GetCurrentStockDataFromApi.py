import pandas.core.frame
from Constans import Companies


def GetCurrentStockDataFromApi(ticker, day, stockSymbol):
    companyWithInfo = Companies.CompaniesWithInfoDict[stockSymbol]
    data: pandas.core.frame.DataFrame = ticker.history(period=f"{day}d", interval='5m')
    if data.empty:
        return {}

    close = round(data["Close"][-1].item(), 3)
    open = round(data["Open"][0].item(), 3)
    high = round(data["High"].max().item(), 3)
    low = round(data["Low"].min().item(), 3)
    volume = int(data["Volume"].sum())
    timestamp = int(data.index[-1].timestamp())

    return {
        'symbol': stockSymbol,
        'currentPrice': round(close, 3),
        'volume': volume,
        'high': high,
        'low': low,
        'openPrice': open,
        'timestamp': timestamp
    }
