import { StyledTitle } from "../../styles/title.styled";
import AddNewItem from "../AddNewItem/AddNewItem";
import CurrentList from "../CurrentList/curentlist";
import { StyledDiv } from "../Home/home";
import Modal from "../Modal/modal";

function Details() {
  return (
    <>
      <div className="details">
        <StyledTitle>Details</StyledTitle>
        <CurrentList details={true} />
        <StyledDiv>
          <Modal
            modalTitle="Add to list"
            modalButtonTitle="Add a new item to list"
          >
            <AddNewItem />
          </Modal>
        </StyledDiv>
      </div>
    </>
  );
}

export default Details;
