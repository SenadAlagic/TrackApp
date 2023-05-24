import { useEffect, useState } from "react";
import {
  Wrapper,
  Button,
  Controls,
  Input,
  SmallButton,
} from "../AddNewItem/AddNewItem";
import { GetCategories } from "../../services/categoryService";
import { addItemToDb } from "../../services/itemListService";

export interface Category {
  id: number;
  name: string;
}

function NewItem() {
  const [categories, setCategories] = useState<Category[]>([]);
  const [selectedCategory, setSelected] = useState<Category>();
  const [unit, setUnit] = useState("");
  const [name, setName] = useState();

  useEffect(() => {
    GetCategories(setCategories);
  }, []);

  function handleNameChange(event: any) {
    setName(event.target.value);
  }
  function handleUnitChange(event: any) {
    setUnit(event.target.value);
  }
  function handleClick(cat: Category) {
    setSelected(cat);
  }
  function AddToDatabase() {
    if (!name || !selectedCategory?.id) return;
    console.log(unit, name, selectedCategory?.id);
    const body = {
      name,
      unit,
      categoryId: selectedCategory?.id,
    };
    addItemToDb(body);
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
        <div className="dropdown-menu" aria-labelledby="dropdownMenuButton">
          {categories.map((category: Category) => (
            <button
              className="dropdown-item"
              onClick={() => handleClick(category)}
            >
              {category.name}
            </button>
          ))}
        </div>
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
            data-bs-dismiss="modal"
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
