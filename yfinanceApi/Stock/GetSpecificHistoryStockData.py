import sys

import pandas.core.frame

from Constans import Companies
import yfinance as yf

from Constans.ErrorMessages import ErrorMessages


def GetSpecificHistoryStockData(stockSymbol:str, period:int,
        periodUnit:str, interval:int, intervalUnit:str):

    if stockSymbol not in Companies.CompaniesSymbol:
        return {
            'error': ErrorMessages.THERE_IS_NO_STOCK_WITH_THIS_SYMBOL
        }

    ticker = yf.Ticker(stockSymbol)
    try:
        data: pandas.core.frame.DataFrame = ticker.history(
            period=f"{period}{periodUnit}",
            interval=f"{interval}{intervalUnit}")

        history_list = []

        dates = data.index.tolist()

        for date in dates:
            date: pandas._libs.tslibs.timestamps.Timestamp
            history_list.append({
                "timestamp": int(date.timestamp()),
                "open": data.loc[date]["Open"],
                "close": data.loc[date]["Close"],
                "high": data.loc[date]["High"],
                "low": data.loc[date]["Low"],
                "volume": int(data.loc[date]["Volume"])
            })
        if history_list.count == 0:
            return {
                "error": ErrorMessages.YOU_SHOULD_START_PYTHON_FILE_AND_SEE_THE_PERIOD_AND_UNIT_RULE
            }

        return history_list


    except Exception as e:
        return {
            'error': str(e)
        }

#print(GetSpecificHistoryStockData("OTP.BD",1,"d",1,"h"))
