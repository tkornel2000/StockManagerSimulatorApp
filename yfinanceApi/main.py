import sys
import threading
import time

import schedule
from flask import Flask, jsonify
from flask_cors import CORS

from Bux.GetCurrentBux import GetCurrentBux
from Bux.GetTodayBux import GetTodayBux
from Data.DbUpdateStockPrice import DbUpdateStockPrice
from Stock.GetCurrentStockPrice import GetCurrentStockPrice
from Stock.GetSpecificHistoryStockData import GetSpecificHistoryStockData

app = Flask(__name__)

DbUpdateStockPrice()

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

schedule.every(15).seconds.do(DbUpdateStockPrice)

def run_schedule():
    while True:
        schedule.run_pending()
        time.sleep(1)

if __name__ == "__main__":
    thread = threading.Thread(target=run_schedule)
    thread.start()
    app.run()
