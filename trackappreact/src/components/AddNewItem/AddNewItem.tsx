import { useEffect, useState } from "react";
import ItemService from "../../services/itemService";
import styled from "styled-components";

export interface Items {
  id: number;
  name: string;
  unit: string;
  categoryId: number;
}

const AddNewItem = ({ callback }: any) => {
  const [items, setItems] = useState<Items[]>([]);
  const [selectedItem, setSelected] = useState<Items>();
  const [quantity, setQuantity] = useState("");

  function handleClick(item: Items) {
    setSelected(item);
  }

  function handleChange(event: any) {
    setQuantity(event.target.value);
  }

  function AddToList() {
    if (!selectedItem) return;
    ItemService.addToList(parseInt(quantity), selectedItem.id, 1).then(() => {
      callback();
    });
  }

  useEffect(() => {
    ItemService.getAllItems(setItems);
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
        <div className="dropdown-menu" aria-labelledby="dropdownMenuButton">
          {items.map((item: Items) => (
            <div key={item.id}>
              <button
                className="dropdown-item"
                onClick={() => handleClick(item)}
              >
                {item.name}
              </button>
            </div>
          ))}
        </div>
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

const Wrapper = styled.div`
  #wrapperAddNewItem {
    width: 100%;
    margin: auto;
  }
`;
const Button = styled.button`
  width: 100%;
`;
const Controls = styled.div`
  margin-top: 1em;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
`;
const Input = styled.input`
  width: 30%;
`;
const SmallButton = styled(Button)`
  width: 30%;
`;
