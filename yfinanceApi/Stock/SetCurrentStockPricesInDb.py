from Constans import Companies
from Data.DbContext import mydb
from Stock.GetCurrentStockPrice import GetCurrentStockPrice
import datetime as dt

def DbUpdateStockPrice():
    sql_insert = (
        'INSERT INTO Stock.dbo.StocksPrices'
            '(stockSymbol, price, volume, dayHigh, dayLow, dayOpen, updateTime) '
        'VALUES '
            '(?, ?, ?, ?, ?, ?, ?)'
    )

    for companySymbol in Companies.CompaniesSymbol:
        result = GetCurrentStockPrice(companySymbol)
        print(result)
        value = (
            result["symbol"],
            result["currentPrice"],
            result["volume"],
            result["high"],
            result["low"],
            result["openPrice"],
            dt.datetime.now()
        )
        cursor = mydb.cursor()
        cursor.execute(sql_insert, value)
        mydb.commit()
        cursor.close()
