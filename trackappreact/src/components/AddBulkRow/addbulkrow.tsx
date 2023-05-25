import { ChangeEvent, useEffect, useState } from "react";
import { Button, Dropdown } from "react-bootstrap";
import {
  Wrapper,
  ItemsByCategory,
  Category,
  Items,
  Input,
  Controls,
} from "../AddNewItem/AddNewItem";
import styled from "styled-components";
import "./addbulkrow.css";
import ItemService from "../../services/itemService";

function AddBulkRow() {
  const [selectedItem, setSelected] = useState<Items>();
  const [quantity, setQuantity] = useState("");
  const [items, setItems] = useState<ItemsByCategory[]>([]);

  useEffect(() => {
    ItemService.getItemsByCategories().then(setItems);
  }, []);
  function handleClick(item: Items) {
    setSelected(item);
  }

  function handleChange(event: ChangeEvent<HTMLInputElement>) {
    setQuantity(event.target.value);
  }

  return (
    <>
      <LocalWrapper className="dropdown">
        <LocalControls id="controls">
          <Button
            className="btn btn-secondary dropdown-toggle"
            type="button"
            id="dropdownMenuButton"
            data-toggle="dropdown"
            aria-haspopup="true"
            aria-expanded="false"
            data-bs-toggle="dropdown"
          >
            {!selectedItem?.name ? "Select an item" : selectedItem.name}
          </Button>
          <LocalDropDown
            className="dropdown-menu"
            aria-labelledby="dropdownMenuButton"
          >
            {items.map((itemByCategory: ItemsByCategory) => (
              <>
                <Category className="dropdown-item">
                  {itemByCategory.categoryName}
                </Category>
                {itemByCategory.items.map((item: Items) => (
                  <button
                    className="dropdown-item"
                    onClick={() => handleClick(item)}
                  >
                    {item.name}
                  </button>
                ))}
              </>
            ))}
          </LocalDropDown>
          <Inputs id="inputs">
            <LocalInput
              type="number"
              className="form-control control"
              placeholder="Quantity"
              onChange={handleChange}
            ></LocalInput>
            <div id="priceInput" className="input-group">
              <LocalInput
                className="form-control control"
                placeholder="Price"
                onChange={handleChange}
              ></LocalInput>
              <span id="currencySpan" className="input-group-text">
                BAM
              </span>
            </div>
            <Buttons id="buttonsBulk">
              <SmallButton className="btn btn-success control" type="button">
                ✓
              </SmallButton>
              <SmallButton className="btn btn-danger control" type="button">
                X
              </SmallButton>
            </Buttons>
          </Inputs>
        </LocalControls>
      </LocalWrapper>
    </>
  );
}

export default AddBulkRow;

const LocalWrapper = styled(Wrapper)`
  display: flex;
  justify-content: center;
`;
const LocalControls = styled(Controls)`
  margin: 0;
  width: 80%;
  display: flex;
  display-wrap: wrap;
`;
const Buttons = styled.div`
  display: flex;
  display-wrap: wrap;
  width: 10%;
`;

const Inputs = styled.div`
  width: 100%;
  margin-top: 5px;
  display: flex;
`;
const LocalInput = styled(Input)`
  width: 50%;
  margin-right: 5px;
`;

const SmallButton = styled.button`
  height: 100%;
  width: 100%;
  margin-left: 2px;
`;

const LocalDropDown = styled(Dropdown)`
  width: 90%;
  overflow: scroll;
  height: 30vh;
`;
