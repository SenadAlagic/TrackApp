import { ChangeEvent, useEffect, useState } from "react";

function ChatInput(props: any) {
  const [user, setUser] = useState("");
  const [message, setMessage] = useState("");

  useEffect(() => {
    setUser(localStorage.getItem("user") || "Anonymous");
  }, []);

  function onSubmit() {
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

  function handleKeyDown(e: any) {
    if (e.key === "Enter") {
      onSubmit();
    }
  }
  return (
    <input
      type="textarea"
      autoComplete="off"
      onChange={onMessageUpdate}
      onKeyDown={handleKeyDown}
      value={message}
      placeholder="Type a message..."
    />
  );
}

export default ChatInput;
