import { appSettings } from "../site";

export async function GetCategories() {
  try {
    const res = await fetch(`${appSettings.apiUrl}/Category/GetCategories`);
    const data = await res.json();
    return data;
  } catch (error) {
    console.log(error);
  }
}
