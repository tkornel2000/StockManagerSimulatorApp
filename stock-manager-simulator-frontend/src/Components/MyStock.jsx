import { Navbar } from "./Navbar";
import { PermissionForComponent } from "./Functions/PermissionForComponent";
import React, { useEffect, useState } from "react";
import { FaSort } from "react-icons/fa";
import { BsInfoCircle } from "react-icons/bs";
import { dotnetApi } from "../api/axios";
import ErrorModal from "../Modals/ErrorModal";
import SuccessModal from "../Modals/SuccessModal";
import explains from "./Constans/explains";

export const MyStock = () => {
  PermissionForComponent();

  const [stocks, setStocks] = useState([]);
  const [sortOrder, setSortOrder] = useState("asc");
  const [sortColumn, setSortColumn] = useState("");
  const [message, setMessage] = useState("");
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [saleQuantities, setSaleQuantities] = useState({});
  const [saleValues, setSaleValues] = useState({});
  const [loading, setLoading] = useState(true);

  const currentUser = JSON.parse(localStorage.getItem("currentUser"));

  const fetchData = async () => {
    const accessToken = localStorage.getItem("accessToken");
    try {
      const response = await dotnetApi.get(
        "Transaction/available-stocks",
        {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        } );
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

  const handleSaleQuantityChange = (symbol, value) => {
    if (value >= 0 && value <= stocks.find(stock => stock.symbol === symbol).quantity) {
      setSaleQuantities((prevState) => ({
        ...prevState,
        [symbol]: parseInt(value)
      }));

      const stock = stocks.find(stock => stock.symbol === symbol);
      const saleValue = parseFloat(value) * stock.current_price;

      setSaleValues(prevState => ({
        ...prevState,
        [symbol]: saleValue
      }));
    }
  };

  const handleSellClick = async (symbol, current_Price, stock_name, all_quantity) => {
    const saleQuantity = saleQuantities[symbol];

    if (saleQuantity === undefined) {
      setMessage("0 db-ot nem tudsz eladni.");
      setIsModalOpen(true);
      return;
    }

    try {
      const currentTime = new Date().getTime();
      const response = await dotnetApi.post("Transaction/sell", {
        symbol: symbol,
        price: current_Price,
        quantity: saleQuantity,
        timestamp: currentTime,
        stock_name: stock_name
      });
      var money = parseFloat(localStorage.getItem('money'))
      console.log("saleQuantity ",saleQuantity)
      console.log("current_Price ",current_Price)
      console.log("current_Price*saleQuantity ",current_Price*saleQuantity)
      money = money+(current_Price*saleQuantity);
      console.log("money ",money)
      localStorage.setItem('money',money);
      console.log(response.data);
      setMessage(response.data);
      setIsModalOpen(true);
      fetchData();

      const saleValue = parseFloat(saleQuantity) * current_Price;
      setSaleValues(prevState => ({
        ...prevState,
        [symbol]: saleValue.toFixed(2)
      }));
    } catch (error) {
      console.log("Error selling stock:", error);
      setMessage(error.message);
      setIsModalOpen(true);
    }
  };

  const getSortedStocks = () => {
    if (!stocks) {
      return [];
    }
    switch (sortColumn) {
      case "stock_name":
        return stocks.sort((a, b) =>
          sortOrder === "asc"
            ? a.stock_name.localeCompare(b.stock_name)
            : b.stock_name.localeCompare(a.stock_name)
        );
      case "quantity":
        return stocks.sort((a, b) =>
          sortOrder === "asc"
            ? a.quantity - b.quantity
            : b.quantity - a.quantity
        );
      case "current_price":
        return stocks.sort((a, b) =>
          sortOrder === "asc"
            ? a.current_price - b.current_price
            : b.current_price - a.current_price
        );
      case "delta_value":
        return stocks.sort((a, b) =>
          sortOrder === "asc"
            ? a.delta_value - b.delta_value
            : b.delta_value - a.delta_value
        );
      case "delta_percent":
        return stocks.sort((a, b) =>
          sortOrder === "asc"
            ? a.delta_percent - b.delta_percent
            : b.delta_percent - a.delta_percent
        );
      case "value":
        return stocks.sort((a, b) =>
          sortOrder === "asc" ? a.value - b.value : b.value - a.value
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

  return (
    <div>
      <Navbar />
      <div className="container">
        <div className="row justify-content-center mt-3">
          <div className="col-12 bg-little-transparent-white">
            <div className="row justify-content-center mt-3">
              <div className="col-11">
              <h1 className="text-center mt-1 mb-3">Összes részvény</h1>
                <h5 className="text-center mb-3">
                  Rendelkezésre álló pénzed:{" "}
                  {currentUser.money.toLocaleString()} Ft
                </h5>
                <h5 className="text-center mb-3">
                  Pénzed részvényekben:{" "}
                  {currentUser.money.toLocaleString()} Ft
                </h5>
                {loading ? (
                  <div className="text-center">Betöltés...</div>
                ) : (
              <table className="sell-table">
            <thead>
              <tr className="tableHead">
                <th onClick={() => handleColumnClick("stock_name")}>
                  Név {renderSortIcon("stock_name")}
                </th>
                <th onClick={() => handleColumnClick("quantity")}>
                  Mennyiség(db) {renderSortIcon("quantity")}
                </th>
                <th onClick={() => handleColumnClick("current_price")}>
                  Ára(Ft) {renderSortIcon("current_price")}
                </th>
                <th onClick={() => handleColumnClick("value")}>
                  Érték(Ft){renderSortIcon("value")}
                </th>
                <th onClick={() => handleColumnClick("delta_value")}>
                  Érték változás(Ft){renderSortIcon("delta_value")}
                </th>
                <th onClick={() => handleColumnClick("delta_percent")}>
                  Érték változás(%){renderSortIcon("delta_percent")}
                </th>
                <th>Eladás mennyisége(db)</th>
                <th>Eladás értéke(Ft)</th>
                <th></th>
              </tr>
            </thead>
            <tbody className="">
              {getSortedStocks().map((stock) => (
                <tr key={stock.symbol}>
                  <td>
                    {stock.stockSymbol} 
                  </td>
                  <td>{stock.quantity}</td>
                  <td></td>
                  <td></td>
                  <td></td>
                  <td></td>
                  <td>
                    <input
                      className="input"
                      type="number"
                      min={0}
                      max={stock.quantity}
                      value={saleQuantities[stock.symbol] || 0}
                      onChange={(e) =>
                        handleSaleQuantityChange(stock.symbol, e.target.value)
                      }
                    />
                  </td>
                  <td>{saleValues[stock.symbol]?saleValues[stock.symbol].toLocaleString():""}</td>
                  <td>
                    <button
                      className="fogomb"
                      onClick={() =>
                        handleSellClick(
                          stock.symbol,
                          stock.current_price,
                          stock.stock_name,
                          stock.quantity
                        )
                      }
                    >
                      Eladás
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>)}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
