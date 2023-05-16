import { ItemsList } from "../components/CurrentList/curentlist";
import { appSettings } from "../site";

export async function fetchData(
  listId: number,
  set: React.Dispatch<React.SetStateAction<ItemsList[]>>
) {
  if (!listId) return;
  try {
    const res = await fetch(
      `${appSettings.apiUrl}/ItemList/GetByList?id=${listId}`
    );
    if (!res.ok) return;
    const data = await res.json();
    set(data);
  } catch (error) {
    console.log(error);
  }
}
