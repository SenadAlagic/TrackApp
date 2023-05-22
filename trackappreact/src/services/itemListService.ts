import { ItemsList } from "../components/CurrentList/curentlist";
import { appSettings } from "../site";

export async function fetchData(
  listId: number,
  filter: boolean,
  set: React.Dispatch<React.SetStateAction<ItemsList[]>>
) {
  if (!listId) return;
  try {
    const res = await fetch(
      `${appSettings.apiUrl}/ItemList/GetByList?id=${listId}&filter=${filter}`
    );
    if (!res.ok) return;
    const data = await res.json();
    set(data);
  } catch (error) {
    console.log(error);
  }
}

export async function fetchItemHistory(
  itemId: number,
  set: React.Dispatch<React.SetStateAction<ItemsList[]>>
) {
  try {
    const res = await fetch(
      `${appSettings.apiUrl}/ItemList/GetItemHistory?itemId=${itemId}`
    );
    if (!res.ok) return;
    const data = await res.json();
    set(data);
  } catch (error) {
    console.log(error);
  }
}

export async function deleteFromList(itemId: number) {
  try {
    const res = await fetch(
      `${appSettings.apiUrl}/ItemList/RemoveFromListByItemId?itemId=${itemId}`,
      {
        method: "DELETE",
      }
    );
    if (!res.ok) return;
  } catch (error) {}
}

export async function addItemToDb(reqBody: any) {
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
