import React, { useState, useEffect, useRef } from "react";
import {
  HubConnection,
  HubConnectionBuilder,
  HubConnectionState,
} from "@microsoft/signalr";

import ChatWindow from "./chatwindow";
import ChatInput from "./chatinput";
import { appSettings } from "../../site";
import { StyledWrapper } from "../../styles/wrapper.styled";

interface Message {
  User: string;
  Message: string;
}

const Chat = () => {
  const [connection, setConnection] = useState<HubConnection>();
  const [chat, setChat] = useState<Message[]>([]);
  const latestChat = useRef<Message[]>();

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

  return (
    <StyledWrapper>
      <ChatInput sendMessage={sendMessage} />
      <hr />
      <ChatWindow chat={chat} />
    </StyledWrapper>
  );
};

export default Chat;
