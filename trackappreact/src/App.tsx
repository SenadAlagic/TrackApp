import React from "react";
import "./App.css";
import { Route, Routes } from "react-router-dom";
import Home from "./components/Home/home";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
    </Routes>
  );

  // return (
  //   <div className="App">
  //     <CurrentList></CurrentList>
  //   </div>
  // );
}

export default App;
