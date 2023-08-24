from Constans.Companies import CompaniesWithInfo
from Data.DbContext import mydb

def SetStcockInDb():
    cursor = mydb.cursor()

    #Először törlünk mindent és csak utána tudunk hozzáadni.
    sql_delete = 'DELETE FROM Stock.dbo.Stocks';
    cursor.execute(sql_delete)

    sql_insert = (
            'INSERT INTO Stock.dbo.Stocks '
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
