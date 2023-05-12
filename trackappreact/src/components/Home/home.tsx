import React, { useState } from "react";
import CurrentList from "../CurrentList/curentlist";

function Home() {
  let [state, setState] = useState("");

  function handleState() {
    console.log(state);
    setState("state changed from child");
  }
  return (
    <a href="/details">
      <CurrentList changeParent={handleState}></CurrentList>
    </a>
  );
}

export default Home;
