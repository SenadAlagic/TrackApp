import { useEffect, useState } from "react";
import Graph from "../Graph/graph";
import FusionCharts from "fusioncharts";
import Charts from "fusioncharts/fusioncharts.charts";
import ReactFC from "react-fusioncharts";
import FusionTheme from "fusioncharts/themes/fusioncharts.theme.fusion";
import { Button } from "react-bootstrap";
import {
  DropDown,
  ItemsByCategory,
  Category,
  Items,
} from "../AddNewItem/AddNewItem";
import ItemService from "../../services/itemService";
import { StyledWrapper } from "../../styles/wrapper.styled";
import { styled } from "styled-components";

ReactFC.fcRoot(FusionCharts, Charts, FusionTheme);

function ItemGraphs() {
  const [selectedItem, setSelected] = useState<Items>();
  const [items, setItems] = useState<ItemsByCategory[]>([]);

  function handleClick(item: Items) {
    setSelected(item);
  }

  useEffect(() => {
    ItemService.getItemsByCategories().then(setItems);
  }, []);
  return (
    <>
      <br />
      <StyledWrapper>
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
        <Graph ReactFC={ReactFC} itemId={selectedItem?.itemId || 0} />
      </StyledWrapper>
    </>
  );
}

export default ItemGraphs;
export const LocalDropDown = styled(DropDown)`
  width: 38%;
`;
