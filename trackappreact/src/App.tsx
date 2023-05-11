import React from "react";
import "./App.css";
import { Route, Routes } from "react-router-dom";
import Home from "./components/Home/home";
import Details from "./components/Details/details";
import NotFound from "./components/NotFound/notfound";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/details" element={<Details />} />
      <Route path="*" element={<NotFound />} />
    </Routes>
  );

  // return (
  //   <div className="App">
  //     <CurrentList></CurrentList>
  //   </div>
  // );
}

export default App;
