import React, { useState } from "react";
import translateErrorMessage from "./TranslateErrorMessage";
import "./Modal.css";

const ErrorModal = ({ show, handleClose, errorHead, errorMessage }) => {
  const [isOpen, setIsOpen] = useState(false);

  React.useEffect(() => {
    setIsOpen(show);
  }, [show]);

  const closeModal = () => {
    setIsOpen(false);
    if (handleClose) {
      handleClose();
    }
  };

  return (
    <>
    <div
      className={`overlay${isOpen ? " show" : ""}`}
      onClick={closeModal}
    ></div>
    <div
      className={`modal${isOpen ? " show d-block" : ""}`}
      tabIndex="-1"
      role="dialog"
      aria-labelledby="exampleModalLabel"
      aria-hidden={!isOpen}
    >
      <div className="modal-dialog modal modal-dialog-centered" role="document">
        <div className="modal-content rounded-very-lg bg">
          <div className="bg-error rounded-very-lg">
            <div className="row justify-content-center mt-5 mb-5">
              <div className="col-10 bg-danger rounded-lg mt-2">
                <h1
                  className="mx-auto mb-4 mt-4 text-center"
                  id="errorModelLabel"
                >
                  {errorHead}
                </h1>
              </div>
            </div>
            <hr/>
            <div className="row justify-content-center mt-5 mb-5">
              <div className="col-11 bg-danger rounded-lg">
                <h4 className="mb-3 mt-3 text-center">
                  {translateErrorMessage(errorMessage)}
                </h4>
              </div>
            </div>
            <div className="">
              <div className="text-center">
              <button
                type="button"
                className="btn btn-danger btn-very-lg mt-5 mb-5"
                onClick={closeModal}
              >
                Close
              </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    </>
  );
};

export default ErrorModal;
