import datetime

from Data.DbContext import mydb


def GetLastStockDataFromDb(stockSymbol:str):
    cursor = mydb.cursor()
    sql_select = (
        'SELECT TOP (1) StockSymbol, Price, Volume, '
        'DayHigh, DayLow, DayOpen, UpdateTimeInTimestamp '
        'FROM Stock.dbo.StocksPrices '
        'WHERE StockSymbol = ? '
        'ORDER BY UpdateTimeInTimestamp DESC'
    )

    cursor.execute(sql_select, (stockSymbol,))
    result = cursor.fetchone()
    if result == None:
        return {}

    return {
        'symbol': result[0],
        'currentPrice': round(result[1], 3),
        'volume': result[2],
        'high': round(result[3], 3),
        'low': round(result[4], 3),
        'openPrice': round(result[5], 3),
        'timestamp': result[6]
    }
