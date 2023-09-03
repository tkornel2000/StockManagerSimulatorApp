import datetime

import pandas.core.frame

from Constans import Companies
import yfinance as yf

from Constans.ErrorMessages import ErrorMessages
from Data.DbContext import mydb
from Stock.Methods.GetCurrentStockDataFromApi import GetCurrentStockDataFromApi
from Stock.Methods.GetLastStockDataFromDb import GetLastStockDataFromDb


def SetCurrentStockPricesInDb():
    companiesWithInfo: dict = Companies.CompaniesWithInfoDict

    cursor = mydb.cursor()

    sql_insert = (
        'INSERT INTO Stock.dbo.StocksPrices'
        '(StockSymbol, Price, Volume, DayHigh, DayLow, DayOpen, UpdateTimeInTimestamp) '
        'VALUES '
        '(?, ?, ?, ?, ?, ?, ?)'
    )


    for stockSymbol in companiesWithInfo.keys():
        companyWithInfo: dict = companiesWithInfo[stockSymbol]
        stockFromDb = {}
        stockFromApi = {}
        ticker = yf.Ticker(stockSymbol)
        day = 1
        #run = True
        #while run:
            #try:
        listIsEmpty=True
        while listIsEmpty:
            stockFromApi : dict = GetCurrentStockDataFromApi(ticker, day, stockSymbol)
            if len(stockFromApi) > 0:
                listIsEmpty = False
            day = day+1
            if day > 10:
                print(ErrorMessages.THE_LAST_10_DAYS_THERE_IS_NO_DATA_FOR_THIS_STOCK)
                return {
                    'error': ErrorMessages.THE_LAST_10_DAYS_THERE_IS_NO_DATA_FOR_THIS_STOCK
                }

        stockFromDb = GetLastStockDataFromDb(stockSymbol)
        #print(stockFromApi)
        #print(stockFromDb)
        if stockFromApi != stockFromDb:
            print("frissités történik")
            value = (
                stockFromApi["symbol"],
                stockFromApi["currentPrice"],
                stockFromApi["volume"],
                stockFromApi["high"],
                stockFromApi["low"],
                stockFromApi["openPrice"],
                stockFromApi["timestamp"]
            )
            cursor.execute(sql_insert, value)
        mydb.commit()
        #run = False
            # except:
            #     day = day + 1
            #     if day > 10:
            #         print(ErrorMessages.THE_LAST_10_DAYS_THERE_IS_NO_DATA_FOR_THIS_STOCK)
            #         return {
            #             'error': ErrorMessages.THE_LAST_10_DAYS_THERE_IS_NO_DATA_FOR_THIS_STOCK
            #         }
