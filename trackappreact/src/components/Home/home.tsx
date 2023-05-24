import AddNewItem from "../AddNewItem/AddNewItem";
import CurrentList, { ItemsList } from "../CurrentList/curentlist";
import CustomModal from "../Modal/modal";
import { StyledTitle } from "../../styles/title.styled";
import { A } from "../../styles/a.styled";
import styled from "styled-components";
import { useEffect, useState } from "react";
import { fetchData } from "../../services/itemListService";
import {
  fetchCurrentWorkingList,
  fetchTotalPrice,
} from "../../services/listService";
import NewItem from "../NewItem/newitem";

function Home() {
  const [items, setItems] = useState<ItemsList[]>([]);
  const [currentListId, setCurrentList] = useState(0);
  const [totalPrice, setTotalPrice] = useState(0);

  useEffect(() => {
    fetchCurrentWorkingList(setCurrentList);
  }, []);

  useEffect(() => {
    fetchData(currentListId, true, setItems);
    fetchTotalPrice(currentListId, setTotalPrice);
  }, [currentListId]);

  const addItem = () => {
    fetchData(currentListId, true, setItems);
  };

  return (
    <>
      <div className="dashboard">
        <StyledTitle>Dashboard</StyledTitle>
        <CurrentList items={items} details={false} totalPrice={totalPrice} />

        <StyledDiv>
          <CustomModal
            modalTitle="Add to list"
            modalButtonTitle="Add a new item to list"
          >
            <AddNewItem callback={addItem} />
          </CustomModal>
        </StyledDiv>
        <br />

        <StyledDiv>
          <CustomModal
            modalTitle="Add a new item to database"
            modalButtonTitle="Add a new item to database"
          >
            <NewItem />
          </CustomModal>
        </StyledDiv>

        {/* <StyledTitle>Previous months</StyledTitle> */}
        {/* <PreviousLists /> */}
      </div>
    </>
  );
}

export default Home;

export const StyledDiv = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;
`;

export const HalvedModal = styled(CustomModal)`
  width: 50%;
`;
