import React, { useState } from "react";
import CurrentList from "../CurrentList/curentlist";

function Details() {
  let [state, setState] = useState("");
  return <CurrentList />;
}

export default Details;
