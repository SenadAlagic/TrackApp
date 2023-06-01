import "./App.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Home from "./components/Home/home";
import NotFound from "./components/NotFound/notfound";
import Restock from "./components/Restock/restock";
import Details from "./components/Details/details";
import History from "./components/History/history";
import Sidenav from "./components/Sidenav/sidenav";
import AddBulk from "./components/AddBulkList/addbulklist";
import ItemGraphs from "./components/ItemGraphs/itemGraphs";
import ViewPurchase from "./components/ViewPurchase/viewpurchase";

function App() {
  return (
    <>
      <BrowserRouter>
        <Sidenav />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/details" element={<Details />} />
          <Route path="/restock/:productId" element={<Restock />}></Route>
          <Route path="/addBulk" element={<AddBulk />}></Route>
          <Route path="history/:productId" element={<History />}></Route>

          {/* graphs currently on hold */}
          <Route path="/graphs" element={<ItemGraphs />}></Route>
          <Route path="purchase/:purchaseId" element={<ViewPurchase />}></Route>
          <Route path="*" element={<NotFound />} />
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
