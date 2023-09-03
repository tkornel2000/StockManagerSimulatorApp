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
        'currentPrice': result[1],
        'volume': result[2],
        'high': result[3],
        'low': result[4],
        'openPrice': result[5],
        'timestamp': result[6]
    }
