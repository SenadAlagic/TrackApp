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
import Login from "./components/Login/login";
import UserStats from "./components/UsersStats/userstats";
import Chat from "./components/Chat/chat";

function App() {
  return (
    <>
      <BrowserRouter>
        <Sidenav />
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route path="/" element={<Home />} />
          <Route path="/details" element={<Details />} />
          <Route path="/restock/:productId" element={<Restock />}></Route>
          <Route path="/addBulk" element={<AddBulk />}></Route>
          <Route path="history/:productId" element={<History />}></Route>

          {/* graphs currently on hold */}
          <Route path="/graphs" element={<ItemGraphs />}></Route>

          <Route path="purchase/:purchaseId" element={<ViewPurchase />}></Route>
          <Route path="chat" element={<Chat />}></Route>
          <Route path="userStats" element={<UserStats />}></Route>
          <Route path="*" element={<NotFound />} />
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
