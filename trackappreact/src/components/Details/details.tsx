import React, { useState } from "react";
import CurrentList from "../CurrentList/curentlist";
import "./details.css";

function Details() {
  let [state, setState] = useState("");

  function handleState() {
    console.log(state);
    setState("state changed from child");
  }
  return (
    <>
      <div className="details">
        <h1>Details</h1>
        <CurrentList changeParent={handleState} />
      </div>
    </>
  );
}

export default Details;
