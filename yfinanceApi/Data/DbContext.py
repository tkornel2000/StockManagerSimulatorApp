import mysql.connector
import pyodbc

server = 'KORNEL'
database = 'Stock'
trusted_connection = 'yes'
driver = '{ODBC Driver 17 for SQL Server}'

mydb = pyodbc.connect(
    'DRIVER=' + driver + ';SERVER=' + server + ';DATABASE=' + database + ';Trusted_Connection=' + trusted_connection + ';'
)

# mydb = mysql.connector.connect(
#   host="localhost",
#   user="root",
#   password="admin",
#   database="stock2"
# )

