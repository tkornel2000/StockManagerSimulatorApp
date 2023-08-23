import pandas.core.frame
import yfinance as yf
from flask import jsonify

from Constans import Companies
from Constans.ErrorMessages import ErrorMessages


def GetCurrentStockPrice(stockSymbol:str):
    if stockSymbol not in Companies.CompaniesSymbol:
        return {
            'error': ErrorMessages.THERE_IS_NO_STOCK_WITH_THIS_SYMBOL
        }

    ticker = yf.Ticker(stockSymbol)
    day = 1
    while True:
        try:
            data:pandas.core.frame.DataFrame = ticker.history(period=f"{day}d", interval='5m')
            close = round(data["Close"][-1].item(), 3)
            open = round(data["Open"][0].item(), 3)
            high = round(data["High"].max().item(), 3)
            low = round(data["Low"].min().item(), 3)
            volume = int(data["Volume"].sum())

            for company in Companies.CompaniesWithInfo:
                if company['symbol'] == stockSymbol:
                    return {
                        'symbol': stockSymbol,
                        'name': company['name'],
                        'fullname': company['fullname'],
                        'high': high,
                        'low': low,
                        'currentPrice': round(close, 3),
                        'openPrice': open,
                        'volume': volume
                    }

        except:
            day = day+1
            if day > 10:
                return {
                    'error': ErrorMessages.THE_LAST_10_DAYS_THERE_IS_NO_DATA_FOR_THIS_STOCK
                }
