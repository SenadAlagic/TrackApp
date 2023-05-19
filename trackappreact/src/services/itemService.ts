import { Dispatch } from "react";
import { Items, ItemsByCategory } from "../components/AddNewItem/AddNewItem";
import { appSettings } from "../site";
import { Purchase } from "../components/History/history";

export default class ItemService {
  static getAllItems = async (
    setItems: Dispatch<React.SetStateAction<Items[]>>
  ) => {
    try {
      const res = await fetch(`${appSettings.apiUrl}/Item/GetItems`, {
        method: "GET",
        headers: {
          Accept: "application/json",
        },
      });
      const data = await res.json();
      setItems(data);
    } catch (error) {
      console.log(error);
    }
  };

  static getItemsByCategories = async (
    set: React.Dispatch<React.SetStateAction<ItemsByCategory[]>>
  ) => {
    try {
      const res = await fetch(`${appSettings.apiUrl}/Item/GetItemsByCategory`);
      const data = await res.json();
      set(data);
    } catch (error) {
      console.log(error);
    }
  };

  static addToList = async (qty: number, itemId: number, listId: number) => {
    try {
      const res = await fetch(
        `${appSettings.apiUrl}/ItemList/AddItemToList?Quantity=${qty}&ItemId=${itemId}&ListId=${listId}`,
        {
          method: "POST",
          headers: {
            Accept: "application/json",
          },
        }
      );
      const data = await res.json();
      if (!data) alert("Nije uspjesno zavrseno slanje");
    } catch (error) {
      console.log(error);
    }
  };

  static restock = async (reqBody: any) => {
    try {
      const res = await fetch(`${appSettings.apiUrl}/ItemList/Restock`, {
        method: "POST",
        headers: {
          "Content-type": "application/json",
        },
        body: JSON.stringify(reqBody),
      });
      if (!res) return;
    } catch (error) {
      console.log(error);
    }
  };

  static async fetchPurchases(
    itemId: number,
    set: React.Dispatch<React.SetStateAction<Purchase[]>>
  ) {
    try {
      const res = await fetch(
        `${appSettings.apiUrl}/Purchase/GetByItemId?itemId=${itemId}`
      );
      if (!res) return;
      const data = await res.json();
      set(data);
    } catch (error) {
      console.log(error);
    }
  }
}
