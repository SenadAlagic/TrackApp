import { ChangeEvent, useEffect, useState } from "react";
import ItemService from "../../services/itemService";
import styled from "styled-components";
import { addToList } from "../../services/itemListService";

export interface Items {
  itemId: number;
  name: string;
  unit: string;
  categoryId: number;
}

export interface ItemsByCategory {
  categoryName: string;
  items: Items[];
}
const AddNewItem = ({ callback }: { callback: () => void }) => {
  //const [items, setItems] = useState<Items[]>([]);
  const [selectedItem, setSelected] = useState<Items>();
  const [quantity, setQuantity] = useState("");
  const [name, setName] = useState("");

  const [items, setItems] = useState<ItemsByCategory[]>([]);

  function handleClick(item: Items) {
    setSelected(item);
  }

  function handleChange(event: ChangeEvent<HTMLInputElement>) {
    setQuantity(event.target.value);
  }

  function AddToList() {
    if (!selectedItem) return;
    addToList(parseInt(quantity), selectedItem.itemId, name).then(() => {
      callback();
    });
  }

  useEffect(() => {
    //ItemService.getAllItems(setItems);
    ItemService.getItemsByCategories().then(setItems);
    setName(localStorage.getItem("user") || "Anonymous");
  }, []);

  return (
    <>
      <Wrapper id="wrapperAddNewItem" className="dropdown">
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
        <DropDown
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
        </DropDown>
        <Controls id="controls">
          <Input
            type="email"
            className="form-control control"
            placeholder="Quantity"
            onChange={handleChange}
          ></Input>
          <SmallButton
            className="btn btn-primary control"
            type="button"
            data-bs-dismiss="modal"
            onClick={() => AddToList()}
          >
            Confirm
          </SmallButton>
        </Controls>
      </Wrapper>
    </>
  );
};

export default AddNewItem;

export const Wrapper = styled.div`
  width: 100%;
  margin: auto;
`;
export const Button = styled.button`
  width: 100%;
`;
export const Controls = styled.div`
  margin-top: 1em;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
`;
export const Input = styled.input`
  width: 50%;
`;
export const SmallButton = styled(Button)`
  width: 45%;
  height: 5%;
`;

export const Category = styled.button`
  font-weight: bold;
  pointer-events: none;
`;

export const DropDown = styled.div`
  overflow: scroll;
  height: 25vh;
  width: 100%;
`;
