import { Category } from "../components/NewItem/newitem";
import { appSettings } from "../site";

export async function GetCategories(
  set: React.Dispatch<React.SetStateAction<Category[]>>
) {
  try {
    const res = await fetch(`${appSettings.apiUrl}/Category/GetCategories`);
    const data = await res.json();
    set(data);
  } catch (error) {
    console.log(error);
  }
}
