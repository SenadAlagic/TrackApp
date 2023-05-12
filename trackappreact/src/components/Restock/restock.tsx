import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Items } from "../AddNewItem/AddNewItem";
import ItemService from "../../services/itemService";
import { useNavigate } from "react-router-dom";
import "./restock.css";

function Restock() {
  const { productId } = useParams();
  const [item, setItem] = useState<Items>();
  const [quantity, setQuantity] = useState();
  const [price, setPrice] = useState();
  const navigate = useNavigate();

  function changeQuantity(event: any) {
    setQuantity(event.target.value);
  }
  function changePrice(event: any) {
    setPrice(event.target.value);
  }

  const fetchItem = async () => {
    try {
      const res = await fetch(
        `https://localhost:7280/Item/GetById?id=${productId}`,
        {
          method: "GET",
          headers: {
            Accept: "text/plain",
          },
        }
      );
      const data = await res.json();
      setItem(data);
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    fetchItem();
  }, []);

  const logPurchase = () => {
    let body = {
      itemId: item?.id,
      listId: 1,
      quantity: parseInt(quantity || ""),
      totalPrice: parseInt(price || ""),
    };
    ItemService.restock(body);
    navigate("/details");
  };

  return (
    <>
      <div className="restock">
        <h1>Restock</h1>
        <input
          className="form-control control"
          value={item?.name}
          id="itemName"
        ></input>
        <div className="controls">
          <input
            className="form-control control"
            placeholder="Quantity"
            onChange={changeQuantity}
            value={quantity}
          ></input>
          <input
            className="form-control control"
            placeholder="Price"
            onChange={changePrice}
            value={price}
          ></input>
        </div>
        <button
          className="btn btn-primary control"
          type="button"
          onClick={() => logPurchase()}
        >
          Log purchase
        </button>
      </div>
    </>
  );
}

export default Restock;
