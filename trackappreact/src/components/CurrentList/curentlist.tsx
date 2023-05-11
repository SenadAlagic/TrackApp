import React, { useEffect, useState } from "react";
import "./currentlist.css";
import AddNewItem from "../AddNewItem/AddNewItem";
import Modal from "../Modal/modal";

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
        "https://localhost:7280/ItemList/GetByList?id=1",
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
              <th>Name</th>
              <th>Quantity</th>
              <th>Unit</th>
            </tr>
          </thead>
          <tbody>
            {items.map((item: any) => (
              <>
                <tr>
                  <th>{item.name}</th>
                  <th>{item.quantity}</th>
                  <th>{item.unit}</th>
                </tr>
              </>
            ))}
          </tbody>
        </table>
        <Modal
          modalBody={<AddNewItem callback={fetchData} />}
          modalTitle="Add to list"
          modalButtonTitle="Add a new item to list"
        ></Modal>
      </div>
    </>
  );
};

export default CurrentList;
