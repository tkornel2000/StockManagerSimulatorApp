import "./App.css";
import "bootstrap/dist/css/bootstrap.css";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Login from "./Components/Login";
import Register from "./Components/Register";
import { Dashboard } from "./Components/Dashboard/Dashboard";
import { AuthProvider } from "./Context/AuthContext";
import { Stocks } from "./Components/Stocks";
import { MyStock } from "./Components/MyStock";
import { Rank } from "./Components/Rank";
import { Transactions } from "./Components/Transactions";
import { MyProfile } from "./Components/MyProfile";
import { useEffect } from "react";
import { RequireAuth } from "./RequireAuth";

function App() {

  useEffect(() => {
    const handleBeforeUnload = (event) => {
      localStorage.clear()
    };

    window.addEventListener("beforeunload", handleBeforeUnload);

    return () => {
      // Eseménykezelő eltávolítása, amikor az alkalmazás unmountolódik
      window.removeEventListener("beforeunload", handleBeforeUnload);
    };
  }, []);

  return (
    <BrowserRouter>
      <AuthProvider>
        <div className="bg-img">
          <Routes>
            <Route path="/" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route element={<RequireAuth/>}>
              <Route path="/dashboard" element={<Dashboard />} />
              <Route path="/stocks" element={<Stocks />} />
              <Route path="/transactions" element={<Transactions />} />
              <Route path="/mystock" element={<MyStock />} />
              <Route path="/rank" element={<Rank />} />
              <Route path="/myprofile" element={<MyProfile />} />
            </Route>
          </Routes>
        </div>
      </AuthProvider>
    </BrowserRouter>
  );
}

export default App;
