import LogoWithText from "../Images/LogoWithText.png";
import { useState, useContext } from "react";
import AuthContext from "../Context/AuthContext";
import axios from "../api/axios";
import { useNavigate } from "react-router-dom";
import ErrorModal from "../Modals/ErrorModal";

const Login = () => {
  const { setAuth } = useContext(AuthContext);
  const [login, setLogin] = useState({ email: "", password: "" });
  const [showErrorModal, setShowErrorModal] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");
  const navigate = useNavigate();

  const handleLogin = async () => {
    try {
      const response = await axios.post("Auth/login", {
        email: login.email,
        password: login.password,
      });

      if (response.status === 200) {
        const accessToken = response?.data?.token;
        localStorage.setItem("accessToken", accessToken);
        const responseMe = await axios.get("User/me", {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        });
        const user = responseMe?.data;
        localStorage.setItem("currentUser", JSON.stringify(user));
        setAuth({ user, accessToken });
        navigate("/dashboard");
      }
    } catch (error) {
      if (error?.response?.status === 401) {
        setErrorMessage(error.response.data.error);
      } else {
        setErrorMessage("SERVER_ERROR");
      }
      setShowErrorModal(true);
    }
  };

  const handleClose = () => {
    setShowErrorModal(false);
  };

  return (
      <div className="vh-100 bg-jpg">
        <div className="container">
          <div className="row justify-content-center ">
            <div className="col-5 ">
              <div className="card-body p-5 text-center bg-little-transparent mt-4 rounded-very-lg ">
                <img
                  src={LogoWithText}
                  width="400px"
                  className="mb-3"
                  alt="This is the logo"
                />
                <h1 className="mb-5 fw-bold">Bejelentkezés</h1>
                <form>
                  <div className="mb-3">
                    <input
                      type="email"
                      className="form-control form-control-lg"
                      id="inputEmail"
                      placeholder="E-mail"
                      value={login.email}
                      onChange={(e) =>
                        setLogin({ ...login, email: e.target.value })
                      }
                      autoComplete="on"
                    />
                  </div>
                  <div className="mb-3">
                    <input
                      type="password"
                      className="form-control form-control-lg"
                      id="inputPassword"
                      placeholder="Jelszó"
                      value={login.password}
                      onChange={(e) =>
                        setLogin({ ...login, password: e.target.value })
                      }
                      autoComplete="on"
                    />
                  </div>
                  <button
                    type="button"
                    className="btn btn-lg col-8 btn-primary btn-block mb-4 mt-4"
                    onClick={handleLogin}
                  >
                    Bejelentkezés
                  </button>
                </form>
                <p className="mb-0">Nem regisztráltál még?</p>
                <p>
                  <a
                    href="/register"
                    className="h-4"
                    style={{ fontSize: "18px" }}
                  >
                    Regisztráció
                  </a>
                </p>
            </div>
          </div>
        </div>
      </div>
      <ErrorModal
        show={showErrorModal}
        handleClose={handleClose}
        errorHead="Bejelentkezési hiba"
        errorMessage={errorMessage}
      />
    </div>
  );
};

export default Login;
