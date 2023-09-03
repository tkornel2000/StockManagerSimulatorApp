import pandas.core.frame

import Constans.Companies
from Constans import Companies
import yfinance as yf

from Constans.ErrorMessages import ErrorMessages
from Stock.Methods.GetCurrentStockDataFromApi import GetCurrentStockDataFromApi


def GetCurrentStockPrice(stockSymbol):
    if stockSymbol not in Companies.CompaniesWithInfoDict.keys():
        return {
            "error": ErrorMessages.THERE_IS_NO_STOCK_WITH_THIS_SYMBOL
         }

    ticker = yf.Ticker(stockSymbol)
    day = 1
    listIsEmpty = True
    while listIsEmpty:
        currentStrockData:dict = GetCurrentStockDataFromApi(ticker, day, stockSymbol)
        if len(currentStrockData)>0:
            return currentStrockData
        else:
            day = day + 1
            if day > 10:
                return {
                    'error': ErrorMessages.THE_LAST_10_DAYS_THERE_IS_NO_DATA_FOR_THIS_STOCK
                }