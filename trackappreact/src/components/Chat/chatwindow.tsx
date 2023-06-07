import React from "react";
import Message from "./message";

function ChatWindow(props: any) {
  const chat = props.chat.map((m: any) => (
    <Message
      key={Date.now() * Math.random()}
      user={m.user}
      message={m.message}
    />
  ));

  return (
    <>
      <div className="chat-body">{chat}</div>
    </>
  );
}

export default ChatWindow;
