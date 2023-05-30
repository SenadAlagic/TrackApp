import { Restock } from "../components/AddBulkList/addbulklist";
import { appSettings } from "../site";

export async function fetchData(listId: number, filter: boolean) {
  if (!listId) return;
  try {
    const res = await fetch(
      `${appSettings.apiUrl}/ItemList/GetByList?id=${listId}&filter=${filter}`
    );
    if (!res.ok) return;
    const data = await res.json();
    return data;
  } catch (error) {
    console.log(error);
  }
}

export async function fetchItemHistory(itemId: number) {
  try {
    const res = await fetch(
      `${appSettings.apiUrl}/ItemList/GetItemHistory?itemId=${itemId}`
    );
    if (!res.ok) return;
    const data = await res.json();
    return data;
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

export async function restockInBulk(restockArray: Restock[]) {
  try {
    const res = await fetch(`${appSettings.apiUrl}/ItemList/RestockInBulk`, {
      method: "POST",
      headers: {
        "Content-type": "application/json",
      },
      body: JSON.stringify(restockArray),
    });
    if (!res.ok) return;
  } catch (error) {
    console.log(error);
  }
}
export async function addToList(qty: number, itemId: number, name: string) {
  try {
    const reqbody = {
      quantity: qty,
      itemId: itemId,
      crossedOff: false,
      addedBy: name,
    };
    const res = await fetch(`${appSettings.apiUrl}/ItemList/AddItemToList`, {
      method: "POST",
      headers: {
        "Content-type": "application/json",
      },
      body: JSON.stringify(reqbody),
    });
    const data = await res.json();
    if (!data) alert("Nije uspjesno zavrseno slanje");
  } catch (error) {
    console.log(error);
  }
}

export async function restock(reqBody: any) {
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
}

export async function getDataForDiagrams(
  itemId: number,
  displayInWeeks: boolean
) {
  try {
    const res = await fetch(
      `${appSettings.apiUrl}/ItemList/GetForDiagram?itemId=${itemId}&displayInWeeks=${displayInWeeks}`
    );
    if (!res.ok) return;
    const data = await res.json();
    return data;
  } catch (error) {
    console.log(error);
  }
}
