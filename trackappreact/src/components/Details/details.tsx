import { useEffect, useState } from "react";
import { StyledTitle } from "../../styles/title.styled";
import AddNewItem from "../AddNewItem/AddNewItem";
import CurrentList, { ItemsList } from "../CurrentList/curentlist";
import Modal from "../Modal/modal";
import { fetchData } from "../../services/itemListService";
import {
  fetchCurrentWorkingList,
  fetchTotalPrice,
} from "../../services/listService";
import { StyledDiv } from "../Home/home";

function Details() {
  const [items, setItems] = useState<ItemsList[]>([]);
  const [currentListId, setCurrentList] = useState(0);
  const [totalPrice, setTotalPrice] = useState(0);

  useEffect(() => {
    fetchCurrentWorkingList(setCurrentList);
  }, []);

  useEffect(() => {
    fetchData(currentListId, setItems);
    fetchTotalPrice(currentListId, setTotalPrice);
  }, [currentListId]);

  const addItem = () => {
    fetchData(currentListId, setItems);
  };

  return (
    <>
      <div className="details">
        <StyledTitle>Details</StyledTitle>
        <CurrentList items={items} details={true} totalPrice={totalPrice} />
        <StyledDiv>
          <Modal
            modalTitle="Add to list"
            modalButtonTitle="Add a new item to list"
          >
            <AddNewItem callback={addItem} />
          </Modal>
        </StyledDiv>
      </div>
    </>
  );
}

export default Details;
