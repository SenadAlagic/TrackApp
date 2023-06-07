import React, { useEffect, useRef, useState } from "react";
import { ReactComponent as CloseIcon } from "../../assets/icon_close.svg";
import "./chat.css";
import {
  HubConnection,
  HubConnectionBuilder,
  HubConnectionState,
} from "@microsoft/signalr";
import { appSettings } from "../../site";
import ChatInput from "./chatinput";
import ChatWindow from "./chatwindow";

interface Message {
  User: string;
  Message: string;
}

function Chat() {
  const [connection, setConnection] = useState<HubConnection>();
  const [chat, setChat] = useState<Message[]>([]);
  const latestChat = useRef<Message[]>();
  const [visible, setVisible] = useState(true);

  latestChat.current = chat;

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
      .withUrl(`${appSettings.apiUrl}/hubs/chat`)
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);
  }, []);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then((result) => {
          console.log("Connected!");

          connection.on("ReceiveMessage", (message) => {
            if (!latestChat.current) return;
            const updatedChat = [...latestChat.current];
            updatedChat.push(message);

            setChat(updatedChat);
          });
        })
        .catch((e) => console.log("Connection failed: ", e));
    }
  }, [connection]);

  const sendMessage = async (user: string, message: string) => {
    const chatMessage = {
      user: user,
      message: message,
    };

    if (connection && connection.state === HubConnectionState.Connected) {
      try {
        await connection.send("SendMessage", chatMessage);
      } catch (e) {
        console.log(e);
      }
    } else {
      alert("No connection to server yet.");
    }
  };

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
            {/* <div className="sender-other">
                <div className="sender-name">Senad: </div>
                <div className="other-message">Hi there!</div>
              </div>
              <div className="sender-me">
                <div className="my-message">Hello</div>
              </div> */}
            <ChatWindow chat={chat} />

            <div className="chat-footer">
              <ChatInput sendMessage={sendMessage} />
            </div>
          </div>
        </div>
      )}
    </>
  );
}

export default Chat;
