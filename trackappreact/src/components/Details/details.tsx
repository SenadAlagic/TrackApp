import React, { useState } from "react";
import CurrentList from "../CurrentList/curentlist";

function Details() {
  let [state, setState] = useState("");

  function handleState() {
    console.log(state);
    setState("state changed from child");
  }
  return <CurrentList changeParent={handleState} />;
}

export default Details;
