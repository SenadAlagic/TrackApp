import React, { useEffect, useState } from "react";

function ImprovedMessage(props: any) {
  const [user, setUser] = useState("");

  useEffect(() => {
    setUser(localStorage.getItem("user") || "");
  }, []);

  return (
    <>
      {props.user === user ? (
        <div className="sender-me">
          <div className="my-message">{props.message}</div>
        </div>
      ) : (
        <div className="sender-other">
          <div className="sender-name">{props.user}</div>
          <div className="other-message">{props.message}</div>
        </div>
      )}
    </>
  );
}

export default ImprovedMessage;
