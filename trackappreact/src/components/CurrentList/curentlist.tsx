import React, { useEffect, useState } from "react";
import "./currentlist.css";

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
      <div id="wrapper">
        <h1>{items.length === 0 && <p>No items found</p>}</h1>
        <table className="table">
          <thead>
            <tr>
              <th>itemId</th>
              <th>listId</th>
              <th>quantity</th>
            </tr>
          </thead>
          <tbody>
            {items.map((item: any) => (
              <>
                <tr>
                  <th>{item.itemId}</th>
                  <th>{item.listId}</th>
                  <th>{item.quantity}</th>
                </tr>
              </>
            ))}
          </tbody>
        </table>
      </div>
    </>
  );
};

export default CurrentList;
