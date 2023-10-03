import { Navbar } from "./Navbar";
import React, { useEffect, useState } from "react";
import { FaSort } from "react-icons/fa";
import { dotnetApi } from "../api/axios";

export const Transactions = () => {
  const [transactions, setTransactions] = useState([]);
  const [sortOrder, setSortOrder] = useState("desc");
  const [sortColumn, setSortColumn] = useState("time");
  const [currentPage, setCurrentPage] = useState(1);
  const [itemsPerPage] = useState(15);
  const [isPurchase, setIsPurchase] = useState(null);
  const [loading, setLoading] = useState(true);

  const fetchData = async () => {
    const accessToken = localStorage.getItem("accessToken");
    try {
      const response = await dotnetApi.get("Transaction", {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      });
      const data = response.data;
      console.log(data);
      setTransactions(data);
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

  const getSortedTransactions = () => {
    if (!transactions) {
      return [];
    }

    let filteredTransactions = [];
    if (isPurchase) {
      filteredTransactions = transactions.filter(
        (transaction) => transaction.isPurchase
      );
    } else if (isPurchase === false) {
      filteredTransactions = transactions.filter(
        (transaction) => transaction.isPurchase === false
      );
    } else {
      filteredTransactions = transactions;
    }

    switch (sortColumn) {
      case "time":
        return filteredTransactions.sort((a, b) =>
          sortOrder === "asc"
            ? a.timeInTimestamp - b.timeInTimestamp
            : b.timeInTimestamp - a.timeInTimestamp
        );
      case "stockName":
        return filteredTransactions.sort((a, b) =>
          sortOrder === "asc"
            ? a.stockName.localeCompare(b.stockName)
            : b.stockName.localeCompare(a.stockName)
        );
      case "quantity":
        return filteredTransactions.sort((a, b) =>
          sortOrder === "asc"
            ? a.quantity - b.quantity
            : b.quantity - a.quantity
        );
      case "price":
        return filteredTransactions.sort((a, b) =>
          sortOrder === "asc" ? a.price - b.price : b.price - a.price
        );
      case "value":
        return filteredTransactions.sort((a, b) =>
          sortOrder === "asc"
            ? a.price * a.quantity - b.price * b.quantity
            : b.price * b.quantity - a.price * a.quantity
        );
      case "currentPrice":
        return filteredTransactions.sort((a, b) =>
          sortOrder === "asc"
            ? a.currentPrice - b.currentPrice
            : b.currentPrice - a.currentPrice
        );
      case "currentValue":
        return filteredTransactions.sort((a, b) =>
          sortOrder === "asc"
            ? a.currentPrice * a.quantity - b.currentPrice * b.quantity
            : b.currentPrice * b.quantity - a.currentPrice * a.quantity
        );
      case "isPurchase":
        return filteredTransactions.sort((a, b) =>
          sortOrder === "asc"
            ? a.isPurchase - b.isPurchase
            : b.isPurchase - a.isPurchase
        );
      default:
        return filteredTransactions;
    }
  };

  const handleClickPage = (page) => {
    setCurrentPage(page);
  };

  const filteredTransactions = getSortedTransactions();
  const totalPages = Math.ceil(filteredTransactions.length / itemsPerPage);
  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;
  const currentItems = getSortedTransactions().slice(
    indexOfFirstItem,
    indexOfLastItem
  );

  return (
    <div>
      <Navbar />
      <div className="container" style={{ width: "100%", minHeight: "90vh" }}>
        <div className="row justify-content-center mt-3">
          <div className="col-12 bg-little-transparent-white">
            <div className="row justify-content-center mt-3">
              <div className="col-11">
                <h1 className="text-center mt-1 mb-3">Tranzakciók</h1>
                <div className="row justify-content-center mt-4 mb-4">
                  <div className="col-6 text-center">
                    <button
                      className={`col-2 btn ${
                        isPurchase ? "btn-secondary" : "btn-outline-dark"
                      }`}
                      onClick={() => {
                        setIsPurchase(true);
                        getSortedTransactions();
                        setCurrentPage(1);
                      }}
                    >
                      Vásárlás
                    </button>
                    <button
                      className={`col-2 btn ${
                        isPurchase === false
                          ? "btn-secondary"
                          : "btn-outline-dark"
                      }`}
                      onClick={() => {
                        setIsPurchase(false);
                        getSortedTransactions();
                        setCurrentPage(1);
                      }}
                    >
                      Eladás
                    </button>
                    <button
                      className={`col-2 btn ${
                        isPurchase === null
                          ? "btn-secondary"
                          : "btn-outline-dark"
                      }`}
                      onClick={() => {
                        setIsPurchase(null);
                        getSortedTransactions();
                        setCurrentPage(1);
                      }}
                    >
                      Összes
                    </button>
                  </div>
                </div>
                {loading ? (
                  <div className="text-center">Betöltés...</div>
                ) : (
                  <table className="table table-striped border">
                    <thead>
                      <tr>
                        <th
                          onClick={() => handleColumnClick("time")}
                          style={{ cursor: "pointer" }}
                          className="text-center"
                        >
                          Idő {renderSortIcon("time")}
                        </th>
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
                          {isPurchase
                            ? "Vásásrlás "
                            : isPurchase === false
                            ? "Eladás "
                            : "Vásárlás/eladás "}
                          mennyisége(db) {renderSortIcon("quantity")}
                        </th>
                        <th
                          onClick={() => handleColumnClick("price")}
                          style={{ cursor: "pointer" }}
                          className="text-center"
                        >
                          {isPurchase
                            ? "Vásásrlás "
                            : isPurchase === false
                            ? "Eladás "
                            : "Vásárlás/eladás "}
                          ára(Ft) {renderSortIcon("price")}
                        </th>
                        <th
                          onClick={() => handleColumnClick("currentPrice")}
                          style={{ cursor: "pointer" }}
                          className="text-center"
                        >
                          Jelenlegi ár(Ft){renderSortIcon("currentPrice")}
                        </th>
                        <th
                          onClick={() => handleColumnClick("value")}
                          style={{ cursor: "pointer" }}
                          className="text-center"
                        >
                          {isPurchase
                            ? "Vásásrlás "
                            : isPurchase === false
                            ? "Eladás "
                            : "Vásárlás/eladás "}
                          értéke(Ft){renderSortIcon("value")}
                        </th>
                        <th
                          onClick={() => handleColumnClick("currentValue")}
                          style={{ cursor: "pointer" }}
                          className="text-center"
                        >
                          Jelenlegi értéke(Ft){renderSortIcon("currentValue")}
                        </th>
                        {isPurchase === null ? (
                          <th
                            onClick={() => handleColumnClick("isPurchase")}
                            style={{ cursor: "pointer" }}
                            className="text-center"
                          >
                            {isPurchase
                              ? "Vásásrlás "
                              : isPurchase === false
                              ? "Eladás "
                              : "Vásárlás/eladás "}
                            {renderSortIcon("isPurchase")}
                          </th>
                        ) : null}
                      </tr>
                    </thead>
                    <tbody>
                      {currentItems.map((stock) => (
                        <tr key={stock.symbol}>
                          <td className="text-center">
                            {new Date(
                              stock.timeInTimestamp * 1000
                            ).toLocaleDateString()}{" "}
                            {new Date(
                              stock.timeInTimestamp * 1000
                            ).toLocaleTimeString()}
                          </td>
                          <td className="text-center">{stock.stockName}</td>
                          <td className="text-center">
                            {stock.quantity.toLocaleString()}
                          </td>
                          <td className="text-center">
                            {stock.price.toLocaleString()}
                          </td>
                          <td className="text-center">
                            {stock.currentPrice.toLocaleString()}
                          </td>
                          <td className="text-center">
                            {(stock.quantity * stock.price).toLocaleString()}
                          </td>
                          <td className="text-center">
                            {(
                              stock.quantity * stock.currentPrice
                            ).toLocaleString()}
                          </td>
                          {isPurchase === null?(
                          <td className="text-center">
                            {stock.isPurchase ? "vásárlás" : "eladás"}
                          </td>) 
                          : null}
                        </tr>
                      ))}
                    </tbody>
                  </table>
                )}
                <div className="row justify-content-center mt-4 mb-4">
                  <div className="col-6 text-center">
                    {Array.from({ length: totalPages }, (_, index) => (
                      <button
                        key={index + 1}
                        onClick={() => handleClickPage(index + 1)}
                        className={`col btn ${
                          currentPage === index + 1
                            ? "btn-secondary"
                            : "btn-outline-dark"
                        }`}
                      >
                        {index + 1}
                      </button>
                    ))}
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
