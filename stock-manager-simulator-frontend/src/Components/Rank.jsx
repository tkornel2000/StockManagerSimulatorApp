import { Navbar } from "./Navbar";
import { PermissionForComponent } from "./Functions/PermissionForComponent";
import React, { useEffect, useState } from "react";
import { FaSort } from "react-icons/fa";
import { dotnetApi } from "../api/axios";

export const Rank = () => {
  PermissionForComponent();

  const [ranks, setRanks] = useState([]);
  const [sortOrder, setSortOrder] = useState("asc");
  const [sortColumn, setSortColumn] = useState("place");
  const [currentPage, setCurrentPage] = useState(1);
  const [itemsPerPage] = useState(15);
  const [rankType, setRankType] = useState("Daily");
  const [loading, setLoading] = useState(true);

  const fetchData = async (rankType) => {
    //console.log(rankType)
    setSortColumn("place")
    const accessToken = localStorage.getItem("accessToken");
    try {
      const response = await dotnetApi.get(`Rank/${rankType}`, {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      });
      const data = response.data;
      //console.log(data)
      setRanks(data);
      setLoading(false);
    } catch (error) {
      console.log("Error fetching stock data:", error);
    }
  };

  useEffect(() => {
    fetchData(rankType);
  }, [rankType]);


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

  const getSortedRanks = () => {
    const sortedRanks = ranks;
    sortedRanks.forEach((rank, index) => {
      rank.deltaValue = rank.currentValue-rank.previousValue;
    });

    sortedRanks.sort((a, b) => b.deltaValue - a.deltaValue);

    sortedRanks.forEach((rank, index) => {
      rank.place = index + 1;
    });
  
    switch (sortColumn) {
      case "money":
        return sortedRanks.sort((a, b) =>
          sortOrder === "asc"
            ? a.userDto.money - b.userDto.money
            : b.userDto.money - a.userDto.money
        );
      case "username":
        return sortedRanks.sort((a, b) =>
          sortOrder === "asc"
            ? a.userDto.username.localeCompare(b.userDto.username)
            : b.userDto.username.localeCompare(a.userDto.username)
        );
      case "place":
        return sortedRanks.sort((a, b) =>
          sortOrder === "asc"
            ? a.place - b.place
            : b.place - a.place
        );
      case "stockValue":
        return sortedRanks.sort((a, b) =>
          sortOrder === "asc"
          ? a.userDto.stockValue - b.userDto.stockValue
          : b.userDto.stockValue - a.userDto.stockValue
        );
      case "portfolioValue":
        return sortedRanks.sort((a, b) =>
          sortOrder === "asc"
            ? a.currentValue - b.currentValue
            : b.currentValue - a.currentValue
        );
      case "deltaValue":
        return sortedRanks.sort((a, b) =>
          sortOrder === "asc"
            ? a.deltaValue - b.deltaValue
            : b.deltaValue - a.deltaValue
        );
      default:
        return sortedRanks;
    }
  };

  const handleClickPage = (page) => {
    setCurrentPage(page);
  };

  const sortedRanks = getSortedRanks();
  const totalPages = Math.ceil(sortedRanks.length / itemsPerPage);
  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;
  const currentItems = getSortedRanks().slice(
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
                <h1 className="text-center mt-1 mb-3">Ranglista</h1>
                <h3 className="text-center">
                    {rankType==="Daily"
                    ?"Napi "
                    :rankType==="Weekly"
                    ?"Heti "
                    :rankType==="Monthly"
                    ?"Havi ":
                    "Teljes "}ranglista
                  </h3>
                <div className="row justify-content-center mt-4 mb-4">
                  <div className="col-8 text-center">
                    <button
                      className={`col-2 btn ${
                        rankType==="Daily" ? "btn-secondary" : "btn-outline-dark"
                      }`}
                      onClick={() => {
                        setRankType("Daily");
                        getSortedRanks();
                        setCurrentPage(1);
                      }}
                    >
                      Napi
                    </button>
                    <button
                      className={`col-2 btn ${
                        rankType === "Weekly"
                          ? "btn-secondary"
                          : "btn-outline-dark"
                      }`}
                      onClick={() => {
                        setRankType("Weekly");
                        getSortedRanks();
                        setCurrentPage(1);
                      }}
                    >
                      Heti
                    </button>
                    <button
                      className={`col-2 btn ${
                        rankType === "Monthly"
                          ? "btn-secondary"
                          : "btn-outline-dark"
                      }`}
                      onClick={() => {
                        setRankType("Monthly");
                        getSortedRanks();
                        setCurrentPage(1);
                      }}
                    >
                      Havi
                    </button>
                    <button
                      className={`col-2 btn ${
                        rankType === "AllTime"
                          ? "btn-secondary"
                          : "btn-outline-dark"
                      }`}
                      onClick={() => {
                        setRankType("AllTime");
                        getSortedRanks();
                        setCurrentPage(1);
                      }}
                    >
                      Teljes
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
                          onClick={() => handleColumnClick("place")}
                          style={{ cursor: "pointer" }}
                          className="text-center"
                        >
                          Helyezés {renderSortIcon("place")}
                        </th>
                        <th
                          onClick={() => handleColumnClick("username")}
                          style={{ cursor: "pointer" }}
                          className="text-center"
                        >
                          Felhasználónév {renderSortIcon("username")}
                        </th>
                        <th
                          onClick={() => handleColumnClick("money")}
                          style={{ cursor: "pointer" }}
                          className="text-center"
                        >
                          Rendelkezésre álló pénz {renderSortIcon("money")}
                        </th>
                        <th
                          onClick={() => handleColumnClick("stockValue")}
                          style={{ cursor: "pointer" }}
                          className="text-center"
                        >
                          Részvények értéke{renderSortIcon("stockValue")}
                        </th>
                        <th
                          onClick={() => handleColumnClick("portfolioValue")}
                          style={{ cursor: "pointer" }}
                          className="text-center"
                        >
                          Portfólió értéke{renderSortIcon("portfolioValue")}
                        </th>
                        <th
                          onClick={() => handleColumnClick("deltaValue")}
                          style={{ cursor: "pointer" }}
                          className="text-center"
                        >
                          Változás (Ft){renderSortIcon("deltaValue")}
                        </th>
                      </tr>
                    </thead>
                    <tbody>
                      {currentItems.map((rank) => (
                        <tr key={rank.place}>
                          <td className="text-center">{rank.place}</td>
                          <td className="text-center">{rank.userDto.username}</td>
                          <td className="text-center">{rank.userDto.money.toLocaleString()}</td>
                          <td className="text-center">{rank.userDto.stockValue.toLocaleString()}</td>
                          <td className="text-center">{rank.currentValue.toLocaleString()}</td>
                          <td className="text-center">{(rank.currentValue-rank.previousValue).toLocaleString()}</td>
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
