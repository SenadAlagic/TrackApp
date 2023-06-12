import { ChangeEvent, useEffect, useState } from "react";
import {
  Wrapper,
  Button,
  Controls,
  Input,
  SmallButton,
  DropDown,
} from "../AddNewItem/AddNewItem";
import { GetCategories } from "../../services/categoryService";
import ItemService from "../../services/itemService";

export interface Category {
  categoryId: number;
  name: string;
}

function NewItem() {
  const [categories, setCategories] = useState<Category[]>([]);
  const [selectedCategory, setSelected] = useState<Category>();
  const [unit, setUnit] = useState("");
  const [name, setName] = useState("");

  useEffect(() => {
    GetCategories().then(setCategories);
  }, []);

  function handleNameChange(event: ChangeEvent<HTMLInputElement>) {
    setName(event.target.value);
  }
  function handleUnitChange(event: ChangeEvent<HTMLInputElement>) {
    setUnit(event.target.value);
  }
  function handleClick(cat: Category) {
    debugger;
    setSelected(cat);
  }
  function AddToDatabase() {
    if (!name || !selectedCategory?.categoryId) return;
    const body = {
      name,
      unit,
      categoryId: selectedCategory?.categoryId,
    };
    ItemService.addItemToDb(body);
    alert("Succesfully saved a new item to database");
  }

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
          {selectedCategory?.name || "Select a category"}
        </Button>
        <DropDown
          className="dropdown-menu"
          aria-labelledby="dropdownMenuButton"
        >
          {categories.map((category: Category) => (
            <button
              className="dropdown-item"
              onClick={() => handleClick(category)}
            >
              {category.name}
            </button>
          ))}
        </DropDown>
        <Controls id="controls">
          <Input
            className="form-control control"
            placeholder="Item name"
            onChange={handleNameChange}
          ></Input>
          <Input
            className="form-control control"
            placeholder="Unit"
            onChange={handleUnitChange}
          ></Input>
          <SmallButton
            className="btn btn-primary control"
            type="button"
            onClick={() => AddToDatabase()}
          >
            Confirm
          </SmallButton>
        </Controls>
      </Wrapper>
    </>
  );
}

export default NewItem;
