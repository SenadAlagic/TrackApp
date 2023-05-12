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
  changeParent?: () => void;
  numberOfResults?: number;
}

const CurrentList = ({ changeParent, numberOfResults = 100 }: Props) => {
  const [items, setItems] = useState<ItemsList[]>([]);
  const navigate = useNavigate();

  const fetchData = async () => {
    try {
      const res = await fetch(
        `https://localhost:7280/ItemList/GetByList?id=1&numberOfResults=${numberOfResults}`,
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
    if (changeParent) changeParent();
  };

  useEffect(() => {
    fetchData();
  }, []);

  function toRestock(id: number) {
    navigate(`/restock/${id}`);
  }

  return (
    <>
      <div id="wrapper" className="CurrentList">
        <h1>{items.length === 0 && <p>No items found</p>}</h1>
        <table className="table table-borderless table-sm">
          <thead>
            <tr>
              <th></th>
              <th>Quantity</th>
              <th>Unit</th>
            </tr>
          </thead>
          <tbody>
            {items.map((item: any) => (
              <>
                <tr>
                  <th>{item.categoryName}</th>
                  <th></th>
                  <th></th>
                </tr>
                {item.items.map((rows: any) => (
                  <tr>
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
          modalBody={<AddNewItem callback={fetchData} />}
          modalTitle="Add to list"
          modalButtonTitle="Add a new item to list"
        ></Modal>
      </div>
    </>
  );
};

export default CurrentList;
