import "./confirm.css";
function Confirm() {
  return (
    <>
      <button className="btn btn-danger confirmDelete">Yes, delete</button>
      <button className="btn btn-outline-secondary confirmDelete">
        No, go back
      </button>
    </>
  );
}

export default Confirm;
