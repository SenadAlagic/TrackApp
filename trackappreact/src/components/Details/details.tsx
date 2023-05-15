import AddNewItem from "../AddNewItem/AddNewItem";
import CurrentList from "../CurrentList/curentlist";
import Modal from "../Modal/modal";

function Details() {
  return (
    <>
      <div className="details">
        <h1>Details</h1>
        <CurrentList details={true} />
        <Modal
          modalTitle="Add to list"
          modalButtonTitle="Add a new item to list"
        >
          <AddNewItem />
        </Modal>
      </div>
    </>
  );
}

export default Details;
