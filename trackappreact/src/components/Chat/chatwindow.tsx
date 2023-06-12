import Message from "./message";
import { IMessage } from "./chat";

interface IChatWindow {
  chat: IMessage[];
}

function ChatWindow({ chat }: IChatWindow) {
  return (
    <div className="chat-body">
      {chat.map(({ user, message }: IMessage) => (
        <Message
          key={Date.now() * Math.random()}
          user={user}
          message={message}
        />
      ))}
    </div>
  );
}

export default ChatWindow;
