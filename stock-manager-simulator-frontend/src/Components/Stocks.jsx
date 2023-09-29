import { Navbar } from "./Navbar";
import React, { useEffect, useState } from "react";
import { FaSort } from "react-icons/fa";
import { BsInfoCircle } from "react-icons/bs";
import { dotnetApi } from "../api/axios";
import ErrorModal from "../Modals/ErrorModal";
import SuccessModal from "../Modals/SuccessModal";
import explains from "./Constans/explains";

export const Stocks = () => {

  const [stockData, setStockData] = useState({});
  const [sortOrder, setSortOrder] = useState("asc");
  const [sortColumn, setSortColumn] = useState("");
  const [stockQuantity, setStockQuantity] = useState({});
  const [loading, setLoading] = useState(true);
  const [showSuccessModal, setShowSuccessModal] = useState(false);
  const [showErrorModal, setShowErrorModal] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");

  const currentUser = JSON.parse(localStorage.getItem("currentUser"));

  const fetchData = async () => {
    const accessToken = localStorage.getItem("accessToken");
    try {
      const response = await dotnetApi.get(
        "Stock",
        {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        } 
      );
      const data = response.data;
      console.log(data);
      setStockData(data);
      setLoading(false);
    } catch (error) {
      console.log("Error fetching stock data:", error);
    }
  };

  useEffect(() => {
    fetchData();

    const interval = setInterval(() => {
      fetchData();
    }, 10 * 60 * 1000);

    return () => {
      clearInterval(interval);
    };
  }, []);

  const handleInputQuantityChange = (symbol, quantity) => {
    console.log(quantity)
    console.log("const newStockQuantity = { [symbol]: quantity };...")
    const newStockQuantity = { [symbol]: quantity };
    console.log("setStockQuantity(newStockQuantity);...")
    setStockQuantity(newStockQuantity);
  };

  const handleBuy = async (symbol, quantity) => {
    console.log(symbol);
    console.log(quantity);
    const accessToken = localStorage.getItem("accessToken");
    try {
      const response = await dotnetApi.post(
        "Transaction/buy",
        {
          stockSymbol: symbol,
          quantity: quantity,
        },
        {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        }
      );
      if (response.status === 204) {
        console.log("Sikeres vásárlás történt.");
        const responseMe = await dotnetApi.get("User/me", {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        });
        const user = responseMe?.data;
        localStorage.setItem("currentUser", JSON.stringify(user));
        setShowSuccessModal(true);
      }
    } catch (error) {
      console.log(error.response);
      if (error?.response?.status === 400) {
        setErrorMessage(error.response.data.error);
      } else {
        setErrorMessage("SERVER_ERROR");
      }
      setShowErrorModal(true);
    }
  };

  const handleColumnClick = (column) => {
    if (sortColumn === column) {
      if (sortOrder === "asc") {
        setSortOrder("desc");
      } else {
        setSortOrder("asc");
      }
    } else {
      setSortColumn(column);
      setSortOrder("asc");
    }
  };

  const getSortedStocks = () => {
    switch (sortColumn) {
      case "name":
        return stockData.sort((stock1, stock2) =>
          sortOrder === "asc"
            ? stock1.name.localeCompare(stock2.name)
            : stock2.name.localeCompare(stock1.name)
        );
      case "price":
        return stockData.sort((stock1, stock2) =>
          sortOrder === "asc"
            ? stock1.price - stock2.price
            : stock2.price - stock1.price
        );
      case "traffic":
        return stockData.sort((stock1, stock2) =>
          sortOrder === "asc"
            ? (stock1.volume * stock1.price) - (stock2.volume * stock2.price)
            : (stock2.volume * stock2.price) - (stock1.volume * stock1.price)
        );
      case "dayHigh":
        return stockData.sort((stock1, stock2) =>
          sortOrder === "asc"
            ? stock1.dayHigh - stock2.dayHigh
            : stock2.dayHigh - stock1.dayHigh
        );
      case "dayLow":
        return stockData.sort((stock1, stock2) =>
          sortOrder === "asc"
            ? stock1.dayLow - stock2.dayLow
            : stock2.dayLow - stock1.dayLow
        );
      case "dayOpen":
        return stockData.sort((stock1, stock2) =>
          sortOrder === "asc"
            ? stock1.dayOpen - stock2.dayOpen
            : stock2.dayOpen - stock1.dayOpen
        );
      case "dayDeltaPrice":
        return stockData.sort((stock1, stock2) =>
          sortOrder === "asc"
            ? stock1.price/stock1.dayOpen - stock2.price/stock2.dayOpen
            : stock2.price/stock2.dayOpen - stock1.price/stock1.dayOpen
        );
      default:
        return stockData;
    }
  };

  const renderSortIcon = (column) => {
    if (sortColumn === column) {
      return sortOrder === "asc" ? (
        <FaSort style={{ marginLeft: "5px" }} />
      ) : (
        <FaSort style={{ marginLeft: "5px", transform: "rotate(180deg)" }} />
      );
    }
    return null;
  };

  const renderHelperIcon = (helperText) => {
    const maxLength = 30;
    var outputText = "";
    var isEnter = false;
    for (let i = 0; i < helperText.length; i++) {
      const letter = helperText[i];

      if (i !== 0 && i % maxLength === 0) {
        isEnter = true;
      }
      if (isEnter && letter === " ") {
        outputText += "\n";
        isEnter = false;
      } else {
        outputText += letter;
      }
    }

    return <BsInfoCircle title={outputText} />;
  };

  const handleErrorClose = () => {
    setShowErrorModal(false);
  };

  const handleSuccessClose = () => {
    setShowSuccessModal(false);
  };

  return(
    <div>
      <Navbar />
      <div className="container" style={{ width: "100%", minHeight: "90vh" }}>
        <div className="row justify-content-center mt-3" style={{width:"100%"}}>
          <div className="col bg-little-transparent-white">
            <div className="row justify-content-center mt-3">
              <div className="col-12">
                <h1 className="text-center mt-1 mb-3">Összes részvény</h1>
                <h5 className="text-center mb-3">
                  Rendelkezésre álló pénzed:{" "}
                  {currentUser.money.toLocaleString()} Ft
                </h5>
                {loading ? (
                  <div className="text-center">Betöltés...</div>
                ) : (
                  <table className="table table-striped border">
                    <thead>
                      <tr>
                        <th
                          style={{ cursor: "pointer" }}
                          onClick={() => handleColumnClick("name")}
                          className="text-center"
                        >
                          Név
                          {renderSortIcon("name")}
                        </th>
                        <th
                          style={{ cursor: "pointer" }}
                          onClick={() => handleColumnClick("price")}
                          className="text-center"
                        >
                          Jelenlegi ár(Ft)
                          {renderHelperIcon(explains.priceExplain)}
                          {renderSortIcon("price")}
                        </th>
                        <th
                          style={{ cursor: "pointer" }}
                          onClick={() => handleColumnClick("traffic")}
                          className="text-center"
                        >
                          Forgalom(m Ft)
                          {renderHelperIcon(explains.volumeExplain)}
                          {renderSortIcon("traffic")}
                        </th>
                        <th
                          style={{ cursor: "pointer" }}
                          onClick={() => handleColumnClick("dayHigh")}
                          className="text-center"
                        >
                          Napi max(Ft)
                          {renderHelperIcon(explains.dailyMaxExplain)}
                          {renderSortIcon("dayHigh")}
                        </th>
                        <th
                          style={{ cursor: "pointer" }}
                          onClick={() => handleColumnClick("dayLow")}
                          className="text-center"
                        >
                          Napi min(Ft)
                          {renderHelperIcon(explains.dailyMinExplain)}
                          {renderSortIcon("dayLow")}
                        </th>
                        <th
                          style={{ cursor: "pointer" }}
                          onClick={() => handleColumnClick("dayOpen")}
                          className="text-center"
                        >
                          Nyitó ár(Ft)
                          {renderHelperIcon(explains.dailyOpenExplain)}
                          {renderSortIcon("dayOpen")}
                        </th>
                        <th
                          style={{ cursor: "pointer" }}
                          onClick={() => handleColumnClick("dayDeltaPrice")}
                          className="text-center"
                        >
                          Árváltozás(%)
                          {renderHelperIcon(explains.dailyDeltaOpenExplain)}
                          {renderSortIcon("dayDeltaPrice")}
                        </th>
                        <th className="text-center">Vásárlási mennyiség(db)</th>
                        <th></th>
                      </tr>
                    </thead>
                    <tbody>
                      {getSortedStocks().map((stock) => (
                        <tr key={stock.stockSymbol}>
                          <td className="text-center">
                              {stock.name}
                          </td>
                          <td className="text-center">{stock.price.toLocaleString()}</td>
                          <td className="text-center">
                            {((stock.volume * stock.price) / 1000000)
                              .toFixed(2)
                              .replace(".", ",")}
                          </td>
                          <td className="text-center">{stock.dayHigh.toLocaleString()}</td>
                          <td className="text-center">{stock.dayLow.toLocaleString()}</td>
                          <td className="text-center">{stock.dayOpen.toLocaleString()}</td>
                          <td className="text-center">
                            {stock.price/stock.dayOpen>1?"+":""}{(Math.round((stock.price/stock.dayOpen-1)*1000)/10).toLocaleString()}
                            </td>
                          <td>
                            <div className="col-md-5 mx-auto">
                              <input
                                type="number"
                                id="typeNumber"
                                className="form-control"
                                min={1}
                                max={Math.floor(
                                  currentUser.money / stock.price
                                )}
                                 value={stockQuantity[stock.stockSymbol] || ""}
                                onChange={(e) => {
                                  console.log(e.target.value)
                                  const newValue = parseInt(e.target.value);
                                  if (
                                    !isNaN(newValue) &&
                                    newValue >= 1 &&
                                    newValue <=
                                      Math.round(
                                        currentUser.money / stock.price
                                    )
                                  ) {
                                    handleInputQuantityChange(
                                      stock.stockSymbol,
                                      newValue
                                    );
                                  }
                                }}
                              />
                            </div>
                          </td>
                          <td>
                            <button
                              className="btn btn-secondary"
                              onClick={(e) =>
                                handleBuy(
                                  stock.stockSymbol,
                                  stockQuantity[stock.stockSymbol]
                                )
                              }
                            >
                              Vásárlás
                            </button>
                          </td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                )}
              </div>
            </div>
          </div>
        </div>
      </div>
      <ErrorModal
        show={showErrorModal}
        onClose={handleErrorClose}
        errorHead="Vásárlási hiba"
        errorMessage={errorMessage}
      />
      <SuccessModal
        show={showSuccessModal}
        onClose={handleSuccessClose}
        successHead="Sikeres vásrlás"
        successMessage="Ön sikeresen vásárolt részvényeket."
      />
    </div>
  );
};
