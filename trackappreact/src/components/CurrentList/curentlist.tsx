import { useEffect, useState } from "react";
import "./currentlist.css";
import AddNewItem from "../AddNewItem/AddNewItem";
import Modal from "../Modal/modal";
import { useNavigate } from "react-router-dom";

export interface ItemsList {
  id: number;
  quantity: string;
  dateCreated: Date;
  dateModified: Date;
  itemId: number;
  listId: number;
}

interface Props {
  numberOfResults?: number;
}

const CurrentList = ({ numberOfResults = 100 }: Props) => {
  const [items, setItems] = useState<ItemsList[]>([]);
  const navigate = useNavigate();

  function toRestock(id: number) {
    navigate(`/restock/${id}`);
  }

  useEffect(() => {
    fetchData().then(setItems);
  }, [items]);

  return (
    <>
      <div id="wrapper" className="CurrentList">
        <h1>{items.length === 0 && <p>No items found</p>}</h1>
        <table className="table table-borderless table-sm">
          <tbody>
            {items.map((item: any) => (
              <>
                <tr>
                  <th>{item.categoryName}</th>
                  <th></th>
                  <th></th>
                </tr>
                {item.items.map((rows: any) => (
                  <tr onClick={() => toRestock(rows.itemId)}>
                    <td className="indent">{rows.name}</td>
                    <td className="smallWidth right">{rows.quantity}</td>
                    <td className="smallWidth left">{rows.unit}</td>
                  </tr>
                ))}
              </>
            ))}
          </tbody>
        </table>
        <Modal
          modalTitle="Add to list"
          modalButtonTitle="Add a new item to list"
        >
          <AddNewItem />
        </Modal>
      </div>
    </>
  );
};

export default CurrentList;

async function fetchData() {
  try {
    const res = await fetch(
      `https://localhost:7280/ItemList/GetByList?id=1&numberOfResults=5`,
      {
        method: "GET",
        headers: {
          Accept: "application/json",
        },
      }
    );
    if (!res.ok) return;
    const data = await res.json();
    return data;
  } catch (error) {
    console.log(error);
  }
}
