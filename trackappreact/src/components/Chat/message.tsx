import { IMessage } from "./chat";

function Message(props: IMessage) {
  return (
    <>
      <div className="sender-other">
        <div className="other-message">
          <strong>{props.user}</strong>
          {": " + props.message}
        </div>
      </div>
    </>
  );
}

export default Message;
