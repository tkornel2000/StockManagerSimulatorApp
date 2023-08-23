import pandas.core.frame
import yfinance as yf

from Constans.ErrorMessages import ErrorMessages


def GetTodayBux():
    ticker = yf.Ticker("^BUX.BD")
    day = 1
    while True:
        try:
            data: pandas.core.frame.DataFrame = ticker.history(period=f"{day}d", interval='5m')
            dates = data.index.tolist()
            print(data)
            values = []
            for date in dates:
                date: pandas._libs.tslibs.timestamps.Timestamp
                value: float = data.loc[date]["Close"]
                values.append({
                    "timestamp": int(date.timestamp()),
                    "value": round(value, 3)
                })
            return values

        except:
            day = day + 1
            if day > 10:
                return {
                    'error': ErrorMessages.THE_LAST_10_DAYS_THERE_IS_NO_DATA_FOR_THIS_STOCK
                }
