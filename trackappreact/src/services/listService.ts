import { appSettings } from "../site";

export async function fetchCurrentWorkingList(
  set: React.Dispatch<React.SetStateAction<number>>
) {
  try {
    const res = await fetch(`${appSettings.apiUrl}/List/GetCurrentWorkingList`);
    if (!res.ok) return;
    const data = await res.json();
    set(data.listId);
    return data.id;
  } catch (error) {
    console.log(error);
  }
}

export async function fetchTotalPrice(
  listId: number,
  set: React.Dispatch<React.SetStateAction<number>>
) {
  if (!listId) return;
  try {
    const res = await fetch(`${appSettings.apiUrl}/List/GetList?id=${listId}`);
    if (!res.ok) return;
    const data = await res.json();
    set(data.totalPrice);
    return data.totalPrice;
  } catch (error) {
    console.log(error);
  }
}
