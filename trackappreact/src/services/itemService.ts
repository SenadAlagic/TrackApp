import { Dispatch } from "react";
import { Items } from "../components/AddNewItem/AddNewItem";

export class ItemService {
  static getAllItems = async (
    setItems: Dispatch<React.SetStateAction<Items[]>>
  ) => {
    try {
      const res = await fetch("https://localhost:7280/Item/GetItems", {
        method: "GET",
        headers: {
          Accept: "text/plain",
        },
      });
      const data = await res.json();
      setItems(data);
    } catch (error) {
      console.log(error);
    }
  };

  static addToList = async (qty: number, itemId: number, listId: number) => {
    try {
      const res = await fetch(
        `https://localhost:7280/ItemList/AddItemToList?Quantity=${qty}&ItemId=${itemId}&ListId=${listId}`,
        {
          method: "POST",
          headers: {
            Accept: "text/plain",
          },
        }
      );
      const data = await res.json();
      if (!data) alert("Nije uspjesno zavrseno slanje");
    } catch (error) {
      console.log(error);
    }
  };
}
