import React, { ReactNode, useState } from "react";
import { ModalTitle } from "react-bootstrap";
import Modal from "react-bootstrap/Modal";
import styled from "styled-components";

interface IModal {
  children: ReactNode;
  modalTitle: string;
  modalButtonTitle: string;
}

function CustomModal({ children, modalTitle, modalButtonTitle }: IModal) {
  const [show, setShow] = useState(false);

  const openModal = () => {
    setShow(true);
  };
  const closeModal = () => {
    setShow(false);
  };

  return (
    <>
      <Button className="btn btn-primary" onClick={openModal}>
        {modalButtonTitle}
      </Button>
      <Modal show={show} onHide={closeModal}>
        <Modal.Header closeButton>
          <ModalTitle>{modalTitle}</ModalTitle>
        </Modal.Header>
        <Modal.Body>{children}</Modal.Body>
        <Modal.Footer>
          <Close className="btn btn-primary" onClick={closeModal}>
            Close
          </Close>
        </Modal.Footer>
      </Modal>
    </>
  );
}

export default CustomModal;

const Button = styled.button`
  width: 40%;
`;
const Close = styled(Button)`
  width: 100%;
`;
