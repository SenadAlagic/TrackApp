export async function FetchRepairs() {
  try {
    const req = await fetch(``);
    const data = await req.json();
    console.log(data);
    return data;
  } catch (error) {
    console.log(error);
  }
}
