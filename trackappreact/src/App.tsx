import React from "react";
import "./App.css";
import { Route, Routes, useParams } from "react-router-dom";
import Home from "./components/Home/home";
import NotFound from "./components/NotFound/notfound";
import Details from "./components/Details/details";
import Restock from "./components/Restock/restock";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/details" element={<Details />} />
      <Route path="/restock/:productId" element={<Restock />}></Route>
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
