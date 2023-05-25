import { appSettings } from "../site";

export async function fetchCurrentWorkingList() {
  try {
    const res = await fetch(`${appSettings.apiUrl}/List/GetCurrentWorkingList`);
    if (!res.ok) return;
    const data = await res.json();
    return data.listId;
  } catch (error) {
    console.log(error);
  }
}

export async function fetchTotalPrice(listId: number) {
  if (!listId) return;
  try {
    const res = await fetch(`${appSettings.apiUrl}/List/GetList?id=${listId}`);
    if (!res.ok) return;
    const data = await res.json();
    return data.totalPrice;
  } catch (error) {
    console.log(error);
  }
}
