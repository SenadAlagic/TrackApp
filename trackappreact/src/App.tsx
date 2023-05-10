import React from "react";
import "./App.css";
import CurrentList from "./components/CurrentList/curentlist";
import AddNewItem from "./components/AddNewItem/AddNewItem";

function App() {
  return (
    <div className="App">
      <CurrentList></CurrentList>
      <AddNewItem></AddNewItem>
    </div>
  );
}

export default App;
