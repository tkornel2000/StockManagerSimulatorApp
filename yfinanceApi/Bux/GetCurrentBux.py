import pandas
import yfinance as yf

from Constans.ErrorMessages import ErrorMessages


def GetCurrentBux():
    ticker = yf.Ticker("^BUX.BD")
    day = 1
    while True:
        try:
            data = ticker.history(period=f"{day}1", interval='5m')
            close = data["Close"][-1].item()
            return {
                "value": round(close, 3)
            }

        except:
            day = day+1
            if day > 10:
                return {
                    'error': ErrorMessages.THE_LAST_10_DAYS_THERE_IS_NO_DATA_FOR_THIS_STOCK
                }
