import { ChangeEvent, useEffect, useState } from "react";
import styled from "styled-components";
import { ReactComponent as IconSend } from "../../assets/icon_send.svg";

function ChatInput(props: any) {
  const [user, setUser] = useState("");
  const [message, setMessage] = useState("");

  useEffect(() => {
    setUser(localStorage.getItem("user") || "Anonymous");
  }, []);

  function onSubmit(e: any) {
    e.preventDefault();

    const isUserProvided = user && user !== "";
    const isMessageProvided = message && message !== "";

    if (isUserProvided && isMessageProvided) {
      props.sendMessage(user, message);
      setMessage("");
    } else {
      alert("Please insert an user and a message.");
    }
  }

  const onMessageUpdate = (e: ChangeEvent<HTMLInputElement>) => {
    setMessage(e.target.value);
  };

  return (
    <LocalForm onSubmit={onSubmit} autoComplete="off">
      {/* <label htmlFor="message">Message:</label> */}
      <br />
      <LocalInput
        type="text"
        id="message"
        name="message"
        placeholder="Message"
        className="form-control"
        value={message}
        onChange={onMessageUpdate}
      />
      <button className="btn btn-primary">
        <IconSend />
      </button>
    </LocalForm>
  );
}

export default ChatInput;

export const LocalInput = styled.input`
  width: 90%;
  height: 10%;
  margin-bottom: 0;
`;
const LocalForm = styled.form`
  padding-top: 2vh;
  display: flex;
`;
