from Constans.Companies import CompaniesWithInfo
from Data.DbContext import mydb

def SetStcockInDb():
    cursor = mydb.cursor()

    sql_insert = (
            'INSERT INTO Stock.dbo.stock '
                '(stockSymbol, name, fullName) '
            'VALUES '
                '(?, ?, ?)'
        )
    for company in CompaniesWithInfo:
        values = (
            company["symbol"],
            company["name"],
            company["fullname"]
        )
        cursor.execute(sql_insert, values)
    mydb.commit()
    cursor.close()

SetStcockInDb()
