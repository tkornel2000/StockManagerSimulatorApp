import datetime

from Data.DbContext import mydb


def GetLastStockDataFromDb(stockSymbol:str):
    cursor = mydb.cursor()
    sql_select = (
        'SELECT TOP (1) stockSymbol, price, volume, '
        'dayHigh, dayLow, dayOpen, updateTime '
        'FROM Stock.dbo.StocksPrices '
        'WHERE stockSymbol = ? '
        'ORDER BY updateTime DESC'
    )

    cursor.execute(sql_select, (stockSymbol,))
    result = cursor.fetchone()

    return {
        'symbol': result[0],
        'currentPrice': result[1],
        'volume': result[2],
        'high': result[3],
        'low': result[4],
        'openPrice': result[5],
        'timestamp': result[6]
    }
