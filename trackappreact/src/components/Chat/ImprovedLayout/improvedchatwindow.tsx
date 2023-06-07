import React, { useState } from "react";
import { ReactComponent as CloseIcon } from "../../../assets/icon_close.svg";
import "./improvedchatwindow.css";

function ImprovedChatWindow() {
  const [visible, setVisible] = useState(true);

  function openChatBubble() {
    var element = document.getElementById("chat-bubble");
    if (!element) return;
    element.classList.toggle("open");
  }
  function closeChatBubble() {
    setVisible(false);
  }
  return (
    <>
      {visible && (
        <div id="chat-bubble">
          <div className="chat-container">
            <div className="chat-header">
              <div className="user-avatar" onClick={() => openChatBubble()}>
                <div className="user-status-info">
                  <a href="#">Chatroom</a>
                </div>
              </div>
              <div className="chat-comm">
                <nav>
                  <a href="#" onClick={() => closeChatBubble()}>
                    <CloseIcon />
                  </a>
                </nav>
              </div>
            </div>
            <div className="chat-body">
              <div className="sender-other">
                <div className="sender-name">Senad: </div>
                <div className="other-message">Hi there!</div>
              </div>
              <div className="sender-me">
                <div className="my-message">Hello</div>
              </div>
            </div>
            <div className="chat-footer">
              <input type="textarea" placeholder="Type a message..." />
            </div>
          </div>
        </div>
      )}
    </>
  );
}

export default ImprovedChatWindow;
