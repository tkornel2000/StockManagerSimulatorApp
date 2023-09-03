import pandas.core.frame
import yfinance as yf

from Constans.ErrorMessages import ErrorMessages


def GetCurrentBux():
    ticker = yf.Ticker("^BUX.BD")
    day = 1
    dataIsEmpty = True
    while dataIsEmpty:
        data: pandas.core.frame.DataFrame = ticker.history(period=f"{day}1", interval='5m')
        if data.empty:
            day = day+1
            if day > 10:
                return {
                    'error': ErrorMessages.THE_LAST_10_DAYS_THERE_IS_NO_DATA_FOR_THIS_STOCK
                }
        else:
            close = data["Close"][-1].item()
            return {
                "value": round(close, 3),
                "timestamp": int(data.index[-1].timestamp())
            }
