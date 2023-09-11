import { PermissionForComponent } from "../Functions/PermissionForComponent";
import { Navbar } from "../Navbar";
import BuxPrice from "./BuxPrice";
import BuxLineChart from "./BuxLineChart";
import { WelcomeUser } from "./WelcomeUser";
import { MostPopularStocks } from "./MostPopularStocks";

export const Dashboard = () => {
  PermissionForComponent();
  

  return (
    <div className="vh-100">
      <Navbar />
      <div className="container">
        <div className="row justify-content-center">
          <div className="col-12 ">
            <div className="card-body p-4 text-center bg-little-transparent-white rounded-very-lg mt-3 row">
              <div className="col mx-1">
                <div className="bg-little-transparent-grey p-4 border rounded border-dark">
                  <WelcomeUser/>
                </div>
                <div className="bg-little-transparent-grey border rounded border-dark mt-4 p-4">
                  <MostPopularStocks/>
                </div>
              </div>
              <div className="buxIndex bg-little-transparent-grey boreder rounded border mx-1 border-dark col">
                <h1 className="">Bux index </h1>
                <div>
                  <BuxPrice />
                </div>
                <div>
                  <BuxLineChart />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
