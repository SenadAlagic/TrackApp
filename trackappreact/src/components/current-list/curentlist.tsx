import React, { useEffect, useState } from "react";

interface ItemsList {
  id: number;
  quantity: string;
  dateCreated: Date;
  dateModified: Date;
  itemId: number;
  listId: number;
}

const CurrentList = () => {
  const [items, setItems] = useState<ItemsList[]>([]);

  const fetchData = async () => {
    try {
      const res = await fetch(
        "https://localhost:7280/ItemList/GetByItemList?id=1",
        {
          method: "GET",
          headers: {
            Accept: "text/plain",
          },
        }
      );
      const data = await res.json();
      console.log(data);
      setItems(data);
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <>
      <h1>{items.length === 0 && <p>No items found</p>}</h1>
      <ul className="list-group">
        {items.map((item: any) => (
          <li key={item.id}>{item.name}</li>
        ))}
      </ul>
    </>
  );
};

export default CurrentList;
