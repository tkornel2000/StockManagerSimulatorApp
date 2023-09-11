import { dotnetApi } from "../../api/axios";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";

export const MostPopularStocks = () => {
  const [stocks, setStocks] = useState([]);

  useEffect(() => {
    dotnetApi.get('Stock')
      .then(response => {
        console.log(response.data)        
        setStocks(response.data);
      })
      .catch(error => {
        console.error('Error fetching data:', error);
      });
  }, []);
  return (
    <div>
      <h3 className="mb-4">Legnépszerűbb részvények</h3>
      <table className="table table-striped border">
        <thead className="table-gray">
          <tr>
            <th scope="col"></th>
            <th scope="col">Részvény</th>
            <th scope="col">Forgalom(m Ft)</th>
            <th scope="col">Jelenlegi ár</th>
          </tr>
        </thead>
        <tbody>
          {stocks
            .sort((stock1, stock2) => stock2.volume * stock2.price - stock1.volume * stock1.price)
            .slice(0, 5)
            .map((stock, index) => (
              <tr key={stock.stockSymbol}>
                <th scope="row">{index + 1}</th>
                <td>{stock.stockSymbol}</td>
                <td>{(Math.round((stock.volume * stock.price / 10000) / 100)).toLocaleString()}</td>
                <td>{stock.price}</td>
              </tr>
            ))}
        </tbody>
      </table>
        <div className="row text-right">
            <Link className="row justify-content-end" to="/stocks">További részvények...</Link>
        </div>
    </div>
  );
};
