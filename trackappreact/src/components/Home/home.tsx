import CurrentList from "../CurrentList/curentlist";
import "./home.css";

function Home() {
  return (
    <>
      <div className="dashboard">
        <h1>Dashboard</h1>
        <CurrentList />
        <br />
        <h1>Previous months</h1>
        {/* <PreviousLists /> */}
      </div>
    </>
  );
}

export default Home;
