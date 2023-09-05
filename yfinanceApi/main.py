import threading
import time

import schedule
from flask import Flask, jsonify

from Bux.GetCurrentBux import GetCurrentBux
from Bux.GetTodayBux import GetTodayBux
from Stock.GetCurrentStockPrice import GetCurrentStockPrice
from Stock.GetSpecificHistoryStockData import GetSpecificHistoryStockData
from Stock.SetCurrentStockPricesInDb import SetCurrentStockPricesInDb
from Stock.SetStockInDb import SetStockInDb

app = Flask(__name__)

#Ha ez meg van hívva akkor törli az összes részvényt a stocks táblából,
#illetve azokat a rekordot is ahol idegen kulcsként szerepel, szóval magyarul majdnem mindent töröl.
#SetStockInDb()
SetCurrentStockPricesInDb()

@app.route('/bux/current')
def GetCurrentBuxFlask():
    return jsonify(GetCurrentBux())

@app.route('/bux/today')
def GetTodayBuxFlask():
    return jsonify(GetTodayBux())

@app.route('/stock/<stockSymbol>')
def GetCurrentStockPriceFlask(stockSymbol):
    currentPrice = GetCurrentStockPrice(stockSymbol)
    return jsonify(currentPrice)

@app.route("/stock/history/<stockSymbol>/<period>/<periodUnit>/<interval>/<intervalUnit>")
def GetSpecificHistoryStockDataFlask(stockSymbol: str, period: int,
        periodUnit: str, interval: int, intervalUnit: str):
    return jsonify(GetSpecificHistoryStockData(stockSymbol, period, periodUnit, interval, intervalUnit))

schedule.every(5).minutes.do(SetCurrentStockPricesInDb)

def run_schedule():
    while True:
        schedule.run_pending()
        time.sleep(1)

if __name__ == "__main__":
    thread = threading.Thread(target=run_schedule)
    thread.start()
    app.run()
