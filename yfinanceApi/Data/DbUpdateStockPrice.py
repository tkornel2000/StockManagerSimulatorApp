from Constans import Companies
from Data.DbContext import mydb
from Stock.GetCurrentStockPrice import GetCurrentStockPrice
import datetime as dt

def DbUpdateStockPrice():
    sql_insert = "INSERT INTO `stock2`.`stock_price` " \
                 "(`stockSymbol`, `price`, `volume`," \
                 "`dayHigh`, `dayLow`, `dayOpen`,`update`) " \
                 "VALUES (%s, %s, %s, %s, %s, %s, %s)"

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


