import { Dispatch } from "react";
import { Items } from "../components/AddNewItem/AddNewItem";
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

  static getItemsByCategories = async () => {
    try {
      const res = await fetch(`${appSettings.apiUrl}/Item/GetItemsByCategory`);
      const data = await res.json();
      return data;
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
  static async addItemToDb(reqBody: any) {
    try {
      const res = await fetch(`${appSettings.apiUrl}/Item/AddItem`, {
        method: "POST",
        headers: {
          "Content-type": "application/json",
        },
        body: JSON.stringify(reqBody),
      });
      if (!res.ok) return;
    } catch (error) {
      console.log(error);
    }
  }
}
