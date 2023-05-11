import React, { useState } from "react";
import CurrentList from "../CurrentList/curentlist";

function Home() {
  let [state, setState] = useState("");

  function handleState() {
    setState("state changed from child");
  }
  return <CurrentList changeParent={handleState}></CurrentList>;
}

export default Home;
