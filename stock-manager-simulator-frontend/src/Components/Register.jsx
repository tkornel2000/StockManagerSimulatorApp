import { useState } from "react";
import axios from "../api/axios";
import ErrorModal from "../Modals/ErrorModal";
import SuccessModal from "../Modals/SuccessModal";

const Register = () => {
  const [user, setUser] = useState({
    username: "",
    email: "",
    firstname: "",
    lastname: "",
    password: "",
    confirmPassword: "",
    birthOfDate: "",
    gender: "",
  });
  const [showErrorModal, setShowErrorModal] = useState(false);
  const [showSuccessModal, setShowSuccessModal] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");

  const handleLogin = async () => {
    try {
      const response = await axios.post("user", {
        username: user.username,
        email: user.email,
        firstname: user.firstname,
        lastname: user.lastname,
        password: user.password,
        confirmPassword: user.confirmPassword,
        birthOfDate: new Date(user.birthOfDate).toISOString(),
        gender: user.gender,
      });

      if (response.status === 204) {
        setShowSuccessModal(true);
      }
    } catch (error) {
      if (error?.response?.status === 400) {
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
            <div className="card-body p-5 pb-4 text-center bg-little-transparent mt-4 rounded-very-lg ">
              <h1 className="mb-5 fw-bold">Regisztráció</h1>
              <form>
                <div className="mb-3">
                  <input
                    type="text"
                    className="form-control form-control-lg"
                    id="inputUsername"
                    placeholder="Felhasználónév"
                    value={user.username}
                    onChange={(e) =>
                      setUser({ ...user, username: e.target.value })
                    }
                    autoComplete="on"
                  />
                </div>
                <div className="mb-3">
                  <input
                    type="email"
                    className="form-control form-control-lg"
                    id="inputEmail"
                    placeholder="E-mail"
                    value={user.email}
                    onChange={(e) =>
                      setUser({ ...user, email: e.target.value })
                    }
                    autoComplete="on"
                  />
                </div>
                <div className="row">
                  <div className="mb-3 col">
                    <input
                      type="text"
                      className="form-control form-control-lg"
                      id="inputLastname"
                      placeholder="Vezetéknév"
                      value={user.lastname}
                      onChange={(e) =>
                        setUser({ ...user, lastname: e.target.value })
                      }
                      autoComplete="on"
                    />
                  </div>
                  <div className="mb-3 col">
                    <input
                      type="text"
                      className="form-control form-control-lg"
                      id="inputFirstname"
                      placeholder="Keresztnév"
                      value={user.firstname}
                      onChange={(e) =>
                        setUser({ ...user, firstname: e.target.value })
                      }
                      autoComplete="on"
                    />
                  </div>
                </div>
                <div className="mb-3">
                  <input
                    type="password"
                    className="form-control form-control-lg"
                    id="inputPassword"
                    placeholder="Jelszó"
                    value={user.password}
                    onChange={(e) =>
                      setUser({ ...user, password: e.target.value })
                    }
                    autoComplete="on"
                  />
                </div>
                <div className="mb-3">
                  <input
                    type="password"
                    className="form-control form-control-lg"
                    id="inputConfirmPassword"
                    placeholder="Jelszó megerősítése"
                    value={user.confirmPassword}
                    onChange={(e) =>
                      setUser({ ...user, confirmPassword: e.target.value })
                    }
                    autoComplete="on"
                  />
                </div>
                <div className="row">
                  <div className="mb-3 col">
                    <input
                      type="date"
                      className="form-control form-control-lg"
                      id="inputBirthOfDate"
                      value={user.birthOfDate}
                      onChange={(e) =>
                        setUser({ ...user, birthOfDate: e.target.value })
                      }
                      autoComplete="on"
                    />
                  </div>
                  <div
                    className="mb-3 col"
                    value={user.gender}
                    onChange={(e) =>
                      setUser({ ...user, gender: e.target.value })
                    }
                  >
                    <select className="form-control-lg">
                      <option value="">Kérem a nemét</option>
                      <option value="Férfi">Férfi</option>
                      <option value="Nő">Nő</option>
                    </select>
                  </div>
                </div>
                <button
                  type="button"
                  className="btn btn-lg col-8 btn-primary btn-block mb-4 mt-4"
                  onClick={handleLogin}
                >
                  Regisztráció
                </button>
              </form>
              <p className="mb-0">Van már fiókod?</p>
              <p>
                <a href="/" className="h-4" style={{ fontSize: "18px" }}>
                  Bejelentkezés
                </a>
              </p>
            </div>
          </div>
        </div>
        <ErrorModal
          show={showErrorModal}
          handleClose={handleClose}
          errorHead="Regisztrációs hiba"
          errorMessage={errorMessage}
        />
        <SuccessModal
          show={showSuccessModal}
          handleClose={handleClose}
          successHead="Sikeres regisztráció"
          successMessage="Ön sikereses regisztrált egy új profilt."
        />
      </div>
    </div>
  );
};

export default Register;
