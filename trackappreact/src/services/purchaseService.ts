import { appSettings } from "../site";

export async function fetchPurchaseById(purchaseId: number) {
  try {
    const req = await fetch(
      `${appSettings.apiUrl}/Purchase/GetPurchaseById?purchaseId=${purchaseId}`
    );
    const data = await req.json();
    console.log(data);
    return data;
  } catch (error) {
    console.log(error);
  }
}
