import React, { useState } from "react";
import CurrentList from "../CurrentList/curentlist";
import "./home.css";

function Home() {
  let [state, setState] = useState("");

  function handleState() {
    console.log(state);
    setState("state changed from child");
  }
  return (
    <>
      <div className="dashboard">
        <h1>Dashboard</h1>
        <a href="/details">
          <CurrentList changeParent={handleState}></CurrentList>
        </a>
      </div>
    </>
  );
}

export default Home;
