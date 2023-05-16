import React, { ReactNode } from "react";
import styled from "styled-components";

interface IModal {
  children: ReactNode;
  modalTitle: string;
  modalButtonTitle: string;
}

function Modal({ children, modalTitle, modalButtonTitle }: IModal) {
  return (
    <>
      <Button
        type="button"
        className="btn btn-primary"
        data-bs-toggle="modal"
        data-bs-target="#exampleModal"
      >
        {modalButtonTitle}
      </Button>

      <div
        className="modal fade"
        id="exampleModal"
        role="dialog"
        aria-labelledby="exampleModalLabel"
        aria-hidden="true"
      >
        <div className="modal-dialog" role="document">
          <div className="modal-content">
            <div className="modal-header">
              <h5 className="modal-title" id="exampleModalLabel">
                {modalTitle}
              </h5>
            </div>
            <div className="modal-body">{children}</div>
            <div className="modal-footer">
              <Close
                type="button"
                className="btn btn-secondary"
                data-bs-dismiss="modal"
              >
                Close
              </Close>
              {/* <button type="button" className="btn btn-primary">
                Save changes
              </button> */}
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default Modal;

const Button = styled.button`
  width: 40%;
`;
const Close = styled(Button)`
  width: 100%;
`;
