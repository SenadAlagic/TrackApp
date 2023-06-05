import { appSettings } from "../site";

export async function FetchUserRequests(top: number) {
  try {
    const req = await fetch(
      `${appSettings.apiUrl}/User/MostFrequentRequests?topX=${top}`
    );
    const data = await req.json();
    console.log(data);
    return data;
  } catch (error) {
    console.log(error);
  }
}
export async function FetchUserPurchases(top: number) {
  try {
    const req = await fetch(
      `${appSettings.apiUrl}/User/MostFrequentBuyers?topX=${top}`
    );
    const data = await req.json();
    console.log(data);
    return data;
  } catch (error) {}
}
