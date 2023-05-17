import React from "react";
import "./App.css";
import { Route, Routes } from "react-router-dom";
import Home from "./components/Home/home";
import NotFound from "./components/NotFound/notfound";
import Restock from "./components/Restock/restock";
import Details from "./components/Details/details";
import History from "./components/History/history";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/details" element={<Details />} />
      <Route path="/restock/:productId" element={<Restock />}></Route>
      <Route path="history/:productId" element={<History />}></Route>
      <Route path="*" element={<NotFound />} />
    </Routes>
  );
}

export default App;
