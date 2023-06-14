import "./confirm.css";

interface IConfirm {
  onDelete: () => void;
  //onGoBack: () => void;
}
function Confirm({ onDelete }: IConfirm) {
  return (
    <>
      <button className="btn btn-danger confirmDelete" onClick={onDelete}>
        Yes, delete
      </button>
      {/* <button
        className="btn btn-outline-secondary confirmDelete"
        onClick={onGoBack}
      >
        No, go back
      </button> */}
    </>
  );
}

export default Confirm;
