import { Navbar } from "./Navbar";
import { PermissionForComponent } from "./Functions/PermissionForComponent";
import React, { useState } from "react";
import ErrorModal from "../Modals/ErrorModal";
import SuccessModal from "../Modals/SuccessModal";
import man from "../Images/man.png";
import woman from "../Images/woman.png";
import { dotnetApi } from "../api/axios";

export const MyProfile = () => {
  PermissionForComponent();
  const currentUser = JSON.parse(localStorage.getItem("currentUser"));

  const [showSuccessModal, setShowSuccessModal] = useState(false);
  const [showErrorModal, setShowErrorModal] = useState(false);
  const [successMessage, setSuccessMessage] = useState("");
  const [successHead, setSuccessHead] = useState("");
  const [errorHead, setErrorHead] = useState("");
  const [errorMessage, setErrorMessage] = useState("");
  const [userPassword, setUserPassword] = useState({
    oldPassword: "",
    password: "",
    confirmPassword: "",
  });
  const [user, setUser] = useState({
    id: currentUser.id,
    username: currentUser.username,
    email: currentUser.email,
    firstname: currentUser.firstname,
    lastname: currentUser.lastname,
    birthOfDate: currentUser.birthOfDate.substring(0, 10),
    isMan: currentUser.isMan,
  });

  const handlePasswordChange = async () => {
    const accessToken = localStorage.getItem("accessToken");
    try {
      const response = await dotnetApi.put(`User/change-password/${user.id}`, 
        {
          oldPassword: userPassword.oldPassword,
          newPassword: userPassword.password,
          confirmPassword: userPassword.confirmPassword
        },
        {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        }
      );
      console.log(response)
      if (response.status === 204) {
        setSuccessHead("Sikeres jelszó változtatás")
        setSuccessMessage("Ön sikeresen megváltoztatta a jelszavát.")
        setShowSuccessModal(true);
      }
    } catch (error) {
      setErrorHead("Jelszó változtatás hiba")
      if (error?.response?.status === 400) {
        setErrorMessage(error.response.data.error);
      } else {
        setErrorMessage("SERVER_ERROR");
      }
      setShowErrorModal(true);
    }
  };

  const handleUserChange = async () => {
    const accessToken = localStorage.getItem("accessToken");
    try {
      if (user.birthOfDate === "" || isNaN(new Date(user.birthOfDate))) {
        setErrorMessage("INVALID_BIRTH_OF_DATE");
        setShowErrorModal(true);
        return;
      }
      const response = await dotnetApi.put(`user/${user.id}`, {
        username: user.username,
        firstname: user.firstname,
        lastname: user.lastname,
        birthOfDate: new Date(user.birthOfDate).toISOString(),
        email: user.email,
        isMan: user.isMan
      },
      {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      }
      );
      console.log(response)
      if (response.status === 200) {
        const responseMe = await dotnetApi.get("User/me", {
          headers: {
            Authorization: `Bearer ${accessToken}`,
          },
        });
        const user = responseMe?.data;
        localStorage.setItem("currentUser", JSON.stringify(user));
        setSuccessHead("Sikeres személyes adat változtatás")
        setSuccessMessage("Ön sikeresen megváltoztatta szemlyes adatait.")
        setShowSuccessModal(true);
      }
    } catch (error) {
      setErrorHead("Sikertelen felhasználói adat változtatás")
      if (error?.response?.status === 400) {
        setErrorMessage(error.response.data.error);
      } else {
        setErrorMessage("SERVER_ERROR");
      }
      setShowErrorModal(true);
    }
  };

  const handleErrorClose = () => {
    setShowErrorModal(false);
  };

  const handleSuccessClose = () => {
    setShowSuccessModal(false);
  };

  return (
    <div>
      <Navbar />
      <div className="container" style={{ width: "100%", minHeight: "90vh" }}>
        <div
          className="row justify-content-center mt-3"
          style={{ width: "100%" }}
        >
          <div className="col bg-little-transparent-white">
            <div className="row justify-content-center mt-3">
              <div className="col-12">
                <h1 className="text-center mt-1 mb-5">Felhasználó</h1>
                <div className="row">
                  <div className="col border-2 border-end bg-little-transparent-grey">
                    <h3 className="text-center mb-4">Profil</h3>
                    <div className="text-center">
                      <img
                        className="rounded-circle mb-3"
                        src={currentUser.isMan ? man : woman}
                        alt="show gender"
                      ></img>
                    </div>
                    <h4 className="text-center">{currentUser.username}</h4>
                    <h6 className="text-center mb-4">{currentUser.email}</h6>
                    <div>
                      <p className="text-center" style={{ fontSize: "20px" }}>
                        Rendelkezésre álló pénz:
                        {"  "}
                        {currentUser.money.toLocaleString()}
                        {"  "}
                        Ft
                      </p>
                      <p className="text-center" style={{ fontSize: "20px" }}>
                        Részvényekben lévő pénz:
                        {"  "}
                        {currentUser.stockValue.toLocaleString()} Ft
                      </p>
                      <p className="text-center" style={{ fontSize: "20px" }}>
                        Portfólió teljes értékez:
                        {"  "}
                        {(
                          currentUser.money + currentUser.stockValue
                        ).toLocaleString()}{" "}
                        Ft
                      </p>
                    </div>
                  </div>
                  <div className="col border-2 border-end bg-little-transparent-grey">
                    <h3 className="text-center mb-4">Személyes adatok</h3>
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
                          required
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
                            onFocus={(e) => (e.target.value = '')}
                            autoComplete="on"
                          />
                        </div>
                        <div
                          className="mb-3 col"
                          value={user.isMan?"Man":"Woman"}
                          onChange={(e) =>
                            setUser({ ...user, isMan: e.target.value==="Man"?true:false})
                          }
                        >
                          <select
                            className="form-control-lg"
                            defaultValue={user.isMan?"Man":"Woman"}
                          >
                            <option value="Man">Férfi</option>
                            <option value="Woman">Nő</option>
                          </select>
                        </div>
                      </div>
                      <div className="text-center">
                        <button
                          type="button"
                          className="btn btn-lg col-8 btn-success btn-block mb-4 mt-4"
                          onClick={handleUserChange}
                        >
                          Mentés
                        </button>
                      </div>
                    </form>
                  </div>
                  <div className="col bg-little-transparent-grey">
                    <h3 className="text-center mb-4">Jelszó változtatás</h3>
                    <form>
                      <div className="mb-3">
                        <input
                          type="password"
                          className="form-control form-control-lg"
                          id="inputOldPassword"
                          placeholder="Régi jelszó"
                          value={userPassword.oldPassword}
                          onChange={(e) =>
                            setUserPassword({
                              ...userPassword,
                              oldPassword: e.target.value,
                            })
                          }
                          autoComplete="on"
                        />
                      </div>
                      <div className="mb-3">
                        <input
                          type="password"
                          className="form-control form-control-lg"
                          id="inputPassword"
                          placeholder="Új jelszó"
                          value={userPassword.password}
                          onChange={(e) =>
                            setUserPassword({
                              ...userPassword,
                              password: e.target.value,
                            })
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
                          value={userPassword.confirmPassword}
                          onChange={(e) =>
                            setUserPassword({
                              ...userPassword,
                              confirmPassword: e.target.value,
                            })
                          }
                          autoComplete="on"
                        />
                      </div>
                      <div className="text-center">
                        <button
                          type="button"
                          className="btn btn-lg col-8 btn-success btn-block mb-4 mt-4"
                          onClick={handlePasswordChange}
                        >
                          Mentés
                        </button>
                      </div>
                    </form>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <ErrorModal
        show={showErrorModal}
        onClose={handleErrorClose}
        errorHead={errorHead}
        errorMessage={errorMessage}
      />
      <SuccessModal
        show={showSuccessModal}
        onClose={handleSuccessClose}
        successHead={successHead}
        successMessage={successMessage}
      />
    </div>
  );
};
