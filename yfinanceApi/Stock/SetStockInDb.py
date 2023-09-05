from Constans.Companies import CompaniesWithInfoDict
from Data.DbContext import mydb

def SetStockInDb():
    cursor = mydb.cursor()

    #Először törlünk mindent és csak utána tudunk hozzáadni.
    sql_delete = 'DELETE FROM Stock.dbo.Stocks WHERE 1=1'
    cursor.execute(sql_delete)

    sql_insert = (
            'INSERT INTO Stock.dbo.Stocks '
                '(stockSymbol, name, fullName) '
            'VALUES '
                '(?, ?, ?)'
        )
    for stockSymbol in CompaniesWithInfoDict.keys():
        values = (
            stockSymbol,
            CompaniesWithInfoDict[stockSymbol]["name"],
            CompaniesWithInfoDict[stockSymbol]["fullname"]
        )
        cursor.execute(sql_insert, values)
    mydb.commit()
    cursor.close()
