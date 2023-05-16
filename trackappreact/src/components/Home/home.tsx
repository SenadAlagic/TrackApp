import AddNewItem from "../AddNewItem/AddNewItem";
import CurrentList from "../CurrentList/curentlist";
import Modal from "../Modal/modal";
import { StyledTitle } from "../../styles/title.styled";
import { A } from "../../styles/a.styled";
import styled from "styled-components";

function Home() {
  return (
    <>
      <div className="dashboard">
        <StyledTitle>Dashboard</StyledTitle>
        <A href="/details">
          <CurrentList details={false} />
        </A>
        <StyledDiv>
          <Modal
            modalTitle="Add to list"
            modalButtonTitle="Add a new item to list"
          >
            <AddNewItem />
          </Modal>
        </StyledDiv>
        <br />
        <StyledTitle>Previous months</StyledTitle>
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
