import { Navbar } from "./Navbar";
import React, { useEffect, useState } from "react";
import { FaSort } from "react-icons/fa";
import { dotnetApi } from "../api/axios";
import ErrorModal from "../Modals/ErrorModal";
import SuccessModal from "../Modals/SuccessModal";

export const MyStock = () => {

  const [stocks, setStocks] = useState([]);
  const [sortOrder, setSortOrder] = useState("asc");
  const [sortColumn, setSortColumn] = useState("");
  const [loading, setLoading] = useState(true);
  const [stockQuantity, setStockQuantity] = useState({});
  const [showSuccessModal, setShowSuccessModal] = useState(false);
  const [showErrorModal, setShowErrorModal] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");

  const currentUser = JSON.parse(localStorage.getItem("currentUser"));

  const fetchData = async () => {
    const accessToken = localStorage.getItem("accessToken");
    try {
      const response = await dotnetApi.get("Transaction/available-stocks", {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      });
      const data = response.data;
      console.log(data);
      setStocks(data);
      setLoading(false);
    } catch (error) {
      console.log("Error fetching stock data:", error);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

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

  const handleInputQuantityChange = (symbol, quantity) => {
    const newStockQuantity = { [symbol]: quantity };
    setStockQuantity(newStockQuantity);
  };
  
  const handleSellClick = async (symbol) => {
    setStockQuantity({})
    const accessToken = localStorage.getItem("accessToken");
    try {
      const response = await dotnetApi.post(
        "Transaction/sell",
        {
          stockSymbol: symbol,
          quantity: stockQuantity[symbol],
        },
        {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        }
      );
      if (response.status === 204) {
        console.log("Sikeres eladás történt.");
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
    fetchData()
  };

  const getSortedStocks = () => {
    if (!stocks) {
      return [];
    }
    switch (sortColumn) {
      case "stockName":
        return stocks.sort((a, b) =>
          sortOrder === "asc"
            ? a.stockName.localeCompare(b.stockName)
            : b.stockName.localeCompare(a.stockName)
        );
      case "quantity":
        return stocks.sort((a, b) =>
          sortOrder === "asc"
            ? a.quantity - b.quantity
            : b.quantity - a.quantity
        );
      case "price":
        return stocks.sort((a, b) =>
          sortOrder === "asc" 
            ? a.price - b.price 
            : b.price - a.price
        );
      case "value":
        return stocks.sort((a, b) =>
          sortOrder === "asc"
            ? a.price * a.quantity - b.price * b.quantity
            : b.price * b.quantity - a.price * a.quantity
        );
      default:
        return stocks;
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
        <div className="row justify-content-center mt-3">
          <div className="col-12 bg-little-transparent-white">
            <div className="row justify-content-center mt-3">
              <div className="col-11">
                <h1 className="text-center mt-1 mb-3">Saját részvényeim</h1>
                <h5 className="text-center mb-3">
                  Rendelkezésre álló pénzed:{" "}
                  {currentUser.money.toLocaleString()} Ft
                </h5>
                <h5 className="text-center mb-3">
                  Pénzed részvényekben:{" "}
                  {currentUser.stockValue.toLocaleString()} Ft
                </h5>
                {loading ? (
                  <div className="text-center">Betöltés...</div>
                ) : (
                  <table className="table table-striped border">
                    <thead>
                      <tr>
                        <th
                          onClick={() => handleColumnClick("stockName")}
                          style={{ cursor: "pointer" }}
                          className="text-center"
                        >
                          Név {renderSortIcon("stockName")}
                        </th>
                        <th
                          onClick={() => handleColumnClick("quantity")}
                          style={{ cursor: "pointer" }}
                          className="text-center"
                        >
                          Mennyiség(db) {renderSortIcon("quantity")}
                        </th>
                        <th
                          onClick={() => handleColumnClick("price")}
                          style={{ cursor: "pointer" }}
                          className="text-center"
                        >
                          Ára(Ft) {renderSortIcon("price")}
                        </th>
                        <th
                          onClick={() => handleColumnClick("value")}
                          style={{ cursor: "pointer" }}
                          className="text-center"
                        >
                          Érték(Ft){renderSortIcon("value")}
                        </th>
                        <th className="text-center">Eladás mennyisége(db)</th>
                        <th className="text-center">Eladás értéke(Ft)</th>
                        <th></th>
                      </tr>
                    </thead>
                    <tbody>
                      {getSortedStocks().map((stock) => (
                        <tr key={stock.stockSymbol}>
                          <td className="text-center">{stock.stockName}</td>
                          <td className="text-center">{stock.quantity}</td>
                          <td className="text-center">{stock.price.toLocaleString()}</td>
                          <td className="text-center">
                            {(stock.quantity * stock.price).toLocaleString()}
                          </td>
                          <td className="text-center">
                            <div className="col-md-5 mx-auto">
                            <input
                              style={{width:"80px"}}
                              className="form-control"
                              type="number"
                              min={0}
                              max={stock.quantity}
                              value={stockQuantity[stock.stockSymbol] || ""}
                              onChange={(e) => {
                                const newValue = parseInt(e.target.value);
                                  if (
                                    !isNaN(newValue) &&
                                    newValue >= 1 &&
                                    newValue <= stock.quantity
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
                          <td className="text-center">
                            {stockQuantity[stock.stockSymbol]*stock.price
                            ?(stockQuantity[stock.stockSymbol]*stock.price).toLocaleString()
                            : ""}
                          </td>
                          <td className="text-center">
                            <button
                              className="btn btn-secondary"
                              onClick={() =>
                                handleSellClick(stock.stockSymbol)
                              }
                            >
                              Eladás
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
        errorHead="Eladási hiba"
        errorMessage={errorMessage}
      />
      <SuccessModal
        show={showSuccessModal}
        onClose={handleSuccessClose}
        successHead="Sikeres vásrlás"
        successMessage="Ön sikeresen adott el részvényeket."
      />
    </div>
  );
};
