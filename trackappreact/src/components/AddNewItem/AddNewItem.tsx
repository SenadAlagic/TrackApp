import { Dispatch, useEffect, useState } from "react";
import "./addnewitem.css";
import { ItemService } from "../../services/itemService";

interface IAddNewItem {
  callback: () => Promise<void>;
}

export interface Items {
  id: number;
  name: string;
  unit: string;
  categoryId: number;
}

const AddNewItem = ({ callback }: IAddNewItem) => {
  const [items, setItems] = useState<Items[]>([]);
  const [selectedItem, setSelected] = useState<Items>();
  const [quantity, setQuantity] = useState("");

  function handleClick(item: Items) {
    setSelected(item);
  }

  function handleChange(event: any) {
    setQuantity(event.target.value);
    console.log(quantity);
  }

  function AddToList() {
    if (!selectedItem) return;
    ItemService.addToList(parseInt(quantity), selectedItem.id, 1);
    callback();
    // onChangeState("newState");
  }

  useEffect(() => {
    ItemService.getAllItems(setItems);
  }, []);

  return (
    <>
      <div id="wrapperAddNewItem" className="dropdown">
        <button
          className="btn btn-secondary dropdown-toggle"
          type="button"
          id="dropdownMenuButton"
          data-toggle="dropdown"
          aria-haspopup="true"
          aria-expanded="false"
          data-bs-toggle="dropdown"
        >
          {!selectedItem?.name ? "Select an item" : selectedItem.name}
        </button>
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
        <div id="controls">
          <input
            type="email"
            className="form-control control"
            placeholder="Quantity"
            onChange={handleChange}
          ></input>
          <button
            className="btn btn-primary control"
            type="button"
            data-bs-dismiss="modal"
            onClick={() => AddToList()}
          >
            Confirm
          </button>
        </div>
      </div>
    </>
  );
};

export default AddNewItem;
