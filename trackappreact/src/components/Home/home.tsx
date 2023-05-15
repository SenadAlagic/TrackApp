import AddNewItem from "../AddNewItem/AddNewItem";
import CurrentList from "../CurrentList/curentlist";
import Modal from "../Modal/modal";

function Home() {
  return (
    <>
      <div className="dashboard">
        <h1>Dashboard</h1>
        <a href="/details">
          <CurrentList />
        </a>
        <Modal
          modalTitle="Add to list"
          modalButtonTitle="Add a new item to list"
        >
          <AddNewItem />
        </Modal>
        <br />
        <h1>Previous months</h1>
        {/* <PreviousLists /> */}
      </div>
    </>
  );
}

export default Home;
