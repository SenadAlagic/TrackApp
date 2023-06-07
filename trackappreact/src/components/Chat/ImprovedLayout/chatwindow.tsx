import React from "react";
import ImprovedMessage from "./message";

function ImprovedChatWindow(props: any) {
  const chat = props.chat.map((m: any) => (
    <ImprovedMessage
      key={Date.now() * Math.random()}
      user={m.user}
      message={m.message}
    />
  ));

  return (
    <>
      <div className="chat-body">
        {chat}
        {/* <div className="sender-me">
          <div className="my-message">Hello</div>
        </div> */}
      </div>
    </>
  );
}

export default ImprovedChatWindow;
